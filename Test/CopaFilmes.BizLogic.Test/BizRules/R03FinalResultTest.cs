using System;
using System.Linq;
using System.Collections.Generic;
using CopaFilmes.BizLogic.BizRules;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using Moq;
using Xunit;

namespace CopaFilmes.BizLogic.Test.BizRules
{
    public class R03FinalResultTest
    {
        private readonly Mock<ITiebreaker> _mockTiebreaker;
        private readonly R03FinalResult _rule;

        public R03FinalResultTest()
        {
            _mockTiebreaker = new Mock<ITiebreaker>();
            _rule = new R03FinalResult(_mockTiebreaker.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new R03FinalResult(null));
        }

        [Fact]
        public void execute_test()
        {
            var dto = _getDto();
            _mockTiebreaker.Setup(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()))
                .Returns(dto.Finals.Movies.OrderByDescending(m => m.AverageRating).ThenBy(m => m.PrimaryTitle));

            _rule.Execute(dto);
            Assert.Equal("Filme 02", dto.CompetitionResult.FirstPlace.PrimaryTitle);
            Assert.Equal("Filme 01", dto.CompetitionResult.SecondPlace.PrimaryTitle);
            Assert.Equal("Filme 04", dto.CompetitionResult.ThirdPlace.PrimaryTitle);

            _mockTiebreaker.Verify(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()), Times.Once);
        }

        private CompetitionBizDto _getDto()
        {
            return new CompetitionBizDto
            {
                SemiFinals = new List<GroupDto>
                {
                    new GroupDto
                    {
                        Movies = new List<Movie>
                        {
                            new Movie {Id = "3", PrimaryTitle = "Filme 03", AverageRating = 1.0},
                            new Movie {Id = "2", PrimaryTitle = "Filme 02", AverageRating = 1.5}
                        }
                    },
                    new GroupDto
                    {
                        Movies = new List<Movie>
                        {
                            new Movie {Id = "4", PrimaryTitle = "Filme 04", AverageRating = 0.5},
                            new Movie {Id = "1", PrimaryTitle = "Filme 01", AverageRating = 1.0}
                        }
                    }
                },
                Finals = new GroupDto
                {
                    Movies = new List<Movie>
                    {
                        new Movie {Id = "1", PrimaryTitle = "Filme 01", AverageRating = 1.0},
                        new Movie {Id = "2", PrimaryTitle = "Filme 02", AverageRating = 1.5}
                    }
                }
            };
        }
    }
}