using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Helpers;
using CopaFilmes.BizLogic.Entities;
using Xunit;

namespace CopaFilmes.BizLogic.Test.BizRules.Helpers
{
    public class TiebreakerAlphabeticalOrderTest
    {
        private readonly TiebreakerAlphabeticalOrder _tiebreaker = new TiebreakerAlphabeticalOrder();

        [Fact]
        public void should_apply_in_alphabetical_order()
        {
            var movies = new List<Movie>
            {
                new Movie{PrimaryTitle = "Movie A", AverageRating = 3.0},
                new Movie{PrimaryTitle = "Movie C", AverageRating = 2.5},
                new Movie{PrimaryTitle = "Movie B", AverageRating = 2.5},
            };

            var result = _tiebreaker.Apply(movies).ToList();
            Assert.NotNull(movies);
            Assert.Equal(movies.Count, result.Count);
            Assert.True(result.First().PrimaryTitle == "Movie A");
            Assert.True(result.Last().PrimaryTitle == "Movie C");
        }
    }
}