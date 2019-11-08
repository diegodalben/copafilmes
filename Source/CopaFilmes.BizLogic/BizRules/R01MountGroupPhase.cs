using System;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Extensions;

namespace CopaFilmes.BizLogic.BizRules
{
    public sealed class R01MountGroupPhase : IBizRule<CompetitionBizDto>
    {
        private const int AmountGroupMovies = 4;
        private readonly IPhase _phase;

        public R01MountGroupPhase(IPhase phase)
        {
            _phase = phase ?? throw new ArgumentNullException();
        }

        public void Execute(CompetitionBizDto dto)
        {
            dto.GroupPhase = dto.SelectedMovies
                .OrderBy(m => m.PrimaryTitle)
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / AmountGroupMovies)
                .Select(x => x.Select(v => v.Value).ToList())
                .Select((x, i) => new GroupDto {Name = $"Grupo {(i+1).ToSequenceChar()}", Movies = x})
                .ToList();

            dto.QuarterFinals = _phase.Dispute(dto.GroupPhase);
        }
    }
}