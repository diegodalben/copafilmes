using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.BizRules.Helpers.Abstraction
{
    public interface ITiebreaker
    {
        IEnumerable<Movie> Apply(IEnumerable<Movie> movies);
    }
}