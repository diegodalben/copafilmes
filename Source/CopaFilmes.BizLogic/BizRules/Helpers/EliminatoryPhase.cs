using System;
using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Extensions;

namespace CopaFilmes.BizLogic.BizRules.Helpers
{
    public sealed class EliminatoryPhase : IPhase
    {
        private const int AmountGroups = 2;
        private readonly ITiebreaker _tiebreaker;

        public EliminatoryPhase(ITiebreaker tiebreaker)
        {
            _tiebreaker = tiebreaker ?? throw new ArgumentNullException();
        }
        
        public IList<GroupDto> Dispute(IEnumerable<GroupDto> groups)
        {
            return groups
                .OrderBy(g => g.Name)
                .Select((g, i) => groups.Skip(i * AmountGroups).Take(AmountGroups).ToList())
                .Where(x => x.Any())
                .Select((g, i) => new GroupDto
                {
                    Name = $"Grupo {(i+1).ToSequenceChar()}",
                    Movies = GetMoviesGroup(g.First(), g.Last())
                })
                .ToList();
        }

        private IList<Movie> GetMoviesGroup(GroupDto groupOne, GroupDto groupTwo)
        {
            return new List<Movie>
            {
                _tiebreaker.Apply(new List<Movie> {groupOne.Movies.First(), groupTwo.Movies.Last()}).First(),
                _tiebreaker.Apply(new List<Movie> {groupTwo.Movies.First(), groupOne.Movies.Last()}).First()
            };
        }
    }
}