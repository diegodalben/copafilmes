using System;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using System.Linq;
using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.BizRules
{
    public sealed class R03FinalResult : IBizRule<CompetitionBizDto>
    {
        private readonly ITiebreaker _tiebreaker;

        public R03FinalResult(ITiebreaker tiebreaker)
        {
            _tiebreaker = tiebreaker ?? throw new ArgumentNullException();
        }

        public void Execute(CompetitionBizDto dto)
        {
            var disputeResult = _tiebreaker.Apply(dto.Finals.Movies).ToList();
            dto.CompetitionResult = new CompetitionResultDto
            {
                FirstPlace = disputeResult.First(),
                SecondPlace = disputeResult.Last(),
                ThirdPlace = GetThirdPlace(dto.SemiFinals, disputeResult.First())
            };
        }

        private static Movie GetThirdPlace(IList<GroupDto> semiFinals, Movie firstPlace)
        {
            var indexFirstPlace = semiFinals.First(x => x.Movies.Any(m => m.Id == firstPlace.Id))
                .Movies.ToList().FindIndex(x => x.Id == firstPlace.Id);

            return semiFinals.First(x => x.Movies.All(m => m.Id != firstPlace.Id))
                .Movies.Select((m, i) => new {Movie = m, Index = i})
                .First(x => x.Index != indexFirstPlace).Movie;
        }
    }
}