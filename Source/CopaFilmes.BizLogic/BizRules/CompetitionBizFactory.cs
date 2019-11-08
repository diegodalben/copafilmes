using System.Collections.Generic;
using System.Collections.ObjectModel;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;

namespace CopaFilmes.BizLogic.BizRules
{
    public class CompetitionBizFactory : IBizRuleFactory<CompetitionBizDto>
    {
        private readonly IPhase _groupPhase;
        private readonly IPhase _eliminatoryPhase;
        private readonly ITiebreaker _tiebreaker;

        public CompetitionBizFactory(IPhase groupPhase, IPhase eliminatoryPhase, ITiebreaker tiebreaker)
        {
            _groupPhase = groupPhase;
            _eliminatoryPhase = eliminatoryPhase;
            _tiebreaker = tiebreaker;
        }

        public IReadOnlyCollection<IBizRule<CompetitionBizDto>> Create()
        {
            var rules = new List<IBizRule<CompetitionBizDto>>
            {
                new R01MountGroupPhase(_groupPhase),
                new R02MountEliminatoryPhase(_eliminatoryPhase),
                new R03FinalResult(_tiebreaker)
            };

            return new ReadOnlyCollection<IBizRule<CompetitionBizDto>>(rules);
        }
    }
}