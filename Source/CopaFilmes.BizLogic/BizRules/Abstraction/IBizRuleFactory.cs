using System.Collections.Generic;

namespace CopaFilmes.BizLogic.BizRules.Abstraction
{
    public interface IBizRuleFactory<in TDto> where TDto : class
    {
        IReadOnlyCollection<IBizRule<TDto>> Create();
    }
}