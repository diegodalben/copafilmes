using System;
using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules.Helpers;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using Moq;
using Xunit;

namespace CopaFilmes.BizLogic.Test.BizRules.Helpers
{
    public class EliminatoryPhaseTest
    {
        private readonly Mock<ITiebreaker> _tiebreaker;
        private readonly EliminatoryPhase _eliminatoryPhase;

        public EliminatoryPhaseTest()
        {
            _tiebreaker = new Mock<ITiebreaker>();
            _eliminatoryPhase = new EliminatoryPhase(_tiebreaker.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new EliminatoryPhase(null));
        }

        [Fact]
        public void dispute_test()
        {
            _tiebreaker.SetupSequence(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()))
                .Returns
                (
                    new List<Movie>
                    {
                        new Movie{PrimaryTitle = "Filme D", AverageRating = 2.5},
                        new Movie{PrimaryTitle = "Filme A", AverageRating = 1.0}
                    }
                )
                .Returns
                (
                    new List<Movie>
                    {
                        new Movie{PrimaryTitle = "Filme C", AverageRating = 2.0},
                        new Movie{PrimaryTitle = "Filme B", AverageRating = 1.5}
                    }
                );

            var groups = _getGroups();
            var result = _eliminatoryPhase.Dispute(groups);
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Grupo A", result.First().Name);
            Assert.Equal("Filme D", result.First().Movies.First().PrimaryTitle);
            Assert.Equal("Filme C", result.First().Movies.Last().PrimaryTitle);

            _tiebreaker.Verify(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()), Times.Exactly(2));
        }

        private IList<GroupDto> _getGroups()
        {
            return new List<GroupDto>
            {
                new GroupDto
                {
                    Name = "Grupo A", 
                    Movies = new List<Movie>
                    {
                        new Movie{PrimaryTitle = "Filme A", AverageRating = 1.0},
                        new Movie{PrimaryTitle = "Filme B", AverageRating = 1.5}
                    }
                },
                new GroupDto
                {
                    Name = "Grupo B", 
                    Movies = new List<Movie>
                    {
                        new Movie{PrimaryTitle = "Filme C", AverageRating = 2.0},
                        new Movie{PrimaryTitle = "Filme D", AverageRating = 2.5}
                    }
                }
            };
        }
    }
}