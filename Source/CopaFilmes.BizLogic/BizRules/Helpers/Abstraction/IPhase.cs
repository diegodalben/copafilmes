using System.Collections.Generic;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.BizRules.Helpers.Abstraction
{
    public interface IPhase
    {
        IList<GroupDto> Dispute(IEnumerable<GroupDto> groups);
    }
}