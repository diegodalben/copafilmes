using System;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;

namespace CopaFilmes.BizLogic.BizRules
{
    public sealed class R02MountEliminatoryPhase : IBizRule<CompetitionBizDto>
    {
        private readonly IPhase _phase;

        public R02MountEliminatoryPhase(IPhase phase)
        {
            _phase = phase ?? throw new ArgumentNullException();
        }

        public void Execute(CompetitionBizDto dto)
        {
            dto.SemiFinals = _phase.Dispute(dto.QuarterFinals);
            dto.Finals = _phase.Dispute(dto.SemiFinals).First();
        }
    }
}