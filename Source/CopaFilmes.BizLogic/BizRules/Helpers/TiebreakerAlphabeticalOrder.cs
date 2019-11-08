using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.BizRules.Helpers
{
    public sealed class TiebreakerAlphabeticalOrder : ITiebreaker
    {
        public IEnumerable<Movie> Apply(IEnumerable<Movie> movies)
        {
            return movies
                .OrderByDescending(m => m.AverageRating)
                .ThenBy(m => m.PrimaryTitle);
        }
    }
}