using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.BizRules;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Dtos;

namespace CopaFilmes.BizLogic.Test.BizRules
{
    public class R02MountEliminatoryPhaseTest
    {
        private readonly Mock<IPhase> _mockPhase;
        private readonly R02MountEliminatoryPhase _rule;

        public R02MountEliminatoryPhaseTest()
        {
            _mockPhase = new Mock<IPhase>();
            _rule = new R02MountEliminatoryPhase(_mockPhase.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new R02MountEliminatoryPhase(null));
        }

        [Fact]
        public void execute_test()
        {
            _mockPhase.SetupSequence(mock => mock.Dispute(It.IsAny<IEnumerable<GroupDto>>()))
                .Returns(_getGroups(2))
                .Returns(_getGroups(1));

            var dto = new Dtos.CompetitionBizDto();
            _rule.Execute(dto);
            Assert.Equal(2, dto.SemiFinals.Count);
            Assert.NotNull(dto.Finals);

            _mockPhase.Verify(mock => mock.Dispute(It.IsAny<IEnumerable<GroupDto>>()), Times.Exactly(2));
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