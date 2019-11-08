using System;
using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.BizRules;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using Moq;
using Xunit;

namespace CopaFilmes.BizLogic.Test.BizRules
{
    public class R01MountGroupPhaseTest
    {
        private readonly Mock<IPhase> _mockPhase;
        private readonly R01MountGroupPhase _rule;

        public R01MountGroupPhaseTest()
        {
            _mockPhase = new Mock<IPhase>();
            _rule = new R01MountGroupPhase(_mockPhase.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new R01MountGroupPhase(null));
        }

        [Fact]
        public void should_separate_list_into_four_groups()
        {
            _mockPhase.SetupSequence(mock => mock.Dispute(It.IsAny<IEnumerable<GroupDto>>()))
                .Returns(_getGroups(4));

            var dto = new CompetitionBizDto{SelectedMovies = _getMovies()};
            _rule.Execute(dto);
            Assert.NotNull(dto.GroupPhase);
            Assert.Equal(4, dto.GroupPhase.Count);
            Assert.Equal(4, dto.QuarterFinals.Count);
            Assert.Equal("Grupo A", dto.GroupPhase[0].Name);
            Assert.Equal("Grupo B", dto.GroupPhase[1].Name);
            Assert.Equal("Grupo C", dto.GroupPhase[2].Name);
            Assert.Equal("Grupo D", dto.GroupPhase[3].Name);
            Assert.Equal("Filme 01", dto.GroupPhase.First().Movies.First().PrimaryTitle);
            Assert.Equal("Filme 16", dto.GroupPhase.Last().Movies.Last().PrimaryTitle);
            
            foreach (var group in dto.GroupPhase)
            {
                Assert.Equal(4, group.Movies.Count());
            }
        }

        private IList<Movie> _getMovies()
        {
            var movies = new List<Movie>();
            for (int i = 16; i > 0; i--)
            {
                movies.Add(new Movie{PrimaryTitle = $"Filme {i:00}"});
            }

            return movies;
        }

        private IList<GroupDto> _getGroups(int amount)
        {
            var groups = new List<GroupDto>();
            for (int i = 0; i < amount; i++)
            {
                groups.Add(new GroupDto());
            }

            return groups;
        }
    }
}