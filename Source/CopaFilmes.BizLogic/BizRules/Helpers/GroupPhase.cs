using System;
using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.BizRules.Helpers
{
    public sealed class GroupPhase : IPhase
    {
        private readonly ITiebreaker _tiebreaker;
        private const int AmountMovies = 2;

        public GroupPhase(ITiebreaker tiebreaker)
        {
            _tiebreaker = tiebreaker ?? throw new ArgumentNullException();
        }

        public IList<GroupDto> Dispute(IEnumerable<GroupDto> groups)
        {
            return groups.Select
            (
                x => new GroupDto
                {
                    Name = x.Name,
                    Movies = _tiebreaker.Apply(x.Movies).Take(AmountMovies)
                } 
            ).ToList();
        }
    }
}