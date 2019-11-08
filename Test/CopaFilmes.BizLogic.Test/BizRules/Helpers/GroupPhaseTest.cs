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
    public class GroupPhaseTest
    {
        private readonly Mock<ITiebreaker> _mockTiebreaker;
        private readonly GroupPhase _groupPhase;

        public GroupPhaseTest()
        {
            _mockTiebreaker = new Mock<ITiebreaker>();
            _groupPhase = new GroupPhase(_mockTiebreaker.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new GroupPhase(null));
        }

        [Fact]
        public void should_return_groups_with_two_movies_each()
        {
            var groups = new List<GroupDto>();
            for (int i = 0; i < 4; i++)
            {
                groups.Add(new GroupDto());
            }

            _mockTiebreaker.Setup(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()))
                .Returns(new List<Movie>
                {
                    new Movie(),
                    new Movie(),
                    new Movie()
                });

            var result = _groupPhase.Dispute(groups).ToList();
            Assert.NotNull(result);
            Assert.Equal(groups.Count, result.Count);
            Assert.True(result.All(g => g.Movies.Count() == 2));

            _mockTiebreaker.Verify(mock => mock.Apply(It.IsAny<IEnumerable<Movie>>()), Times.Exactly(groups.Count));
        }
    }
}