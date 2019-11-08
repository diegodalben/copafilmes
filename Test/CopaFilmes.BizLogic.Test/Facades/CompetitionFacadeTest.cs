using System;
using System.Collections.Generic;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizValidations.Abstraction;
using CopaFilmes.BizLogic.BizValidations;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Facades;
using CopaFilmes.BizLogic.Repositories.Abstraction;
using Moq;
using Xunit;
using System.Collections.ObjectModel;
using FluentValidation;
using FluentValidation.Results;
using AutoFixture.Xunit2;

namespace CopaFilmes.BizLogic.Test.Facades
{
    public class Validator : AbstractValidator<CompetitionBizDto>
    {
        public static bool ValidationFailure {get; set;}

        public Validator()
        {
            if(ValidationFailure)
            {
                RuleFor(dto => dto)
                    .Null();
            }
            else
            {
                RuleFor(dto => dto)
                    .NotNull();
            }
        }
    }

    public class Rule : IBizRule<CompetitionBizDto>
    {
        public void Execute(CompetitionBizDto dto)
        {
            
        }
    }

    public class CompetitionFacadeTest
    {
        private readonly Mock<IBizValidationFactory<CompetitionBizDto>> _mockBizValidationFactory;
        private readonly Mock<IBizRuleFactory<CompetitionBizDto>> _mockBizRuleFactory;
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly CompetitionFacade _facade;

        public CompetitionFacadeTest()
        {
            _mockBizValidationFactory = new Mock<IBizValidationFactory<CompetitionBizDto>>();
            _mockBizRuleFactory = new Mock<IBizRuleFactory<CompetitionBizDto>>();
            _mockMovieRepository = new Mock<IMovieRepository>();
            _facade = new CompetitionFacade
            (
                _mockBizValidationFactory.Object,
                _mockBizRuleFactory.Object,
                _mockMovieRepository.Object
            );
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>
            (
                () => new CompetitionFacade
                (
                    null,
                    _mockBizRuleFactory.Object,
                    _mockMovieRepository.Object
                )
            );

            Assert.Throws<ArgumentNullException>
            (
                () => new CompetitionFacade
                (
                    _mockBizValidationFactory.Object,
                    null,
                    _mockMovieRepository.Object
                )
            );

            Assert.Throws<ArgumentNullException>
            (
                () => new CompetitionFacade
                (
                    _mockBizValidationFactory.Object,
                    _mockBizRuleFactory.Object,
                    null
                )
            );
        }

        [Fact]
        public void should_return_movies()
        {
            _mockMovieRepository.Setup(mock => mock.GetMovies()).Returns(new List<Movie>());

            var result = _facade.GetMovies();
            Assert.NotNull(result);
            Assert.True(result.Ok);
            Assert.NotNull(result.ObjectResponse);

            _mockMovieRepository.Verify(mock => mock.GetMovies(), Times.Once);
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void start_competition_test(bool validationFailure)
        {
            Validator.ValidationFailure = validationFailure;

            _mockBizValidationFactory.Setup(mock => mock.Create())
                .Returns
                (
                    new ReadOnlyCollection<IValidator<CompetitionBizDto>>(new List<IValidator<CompetitionBizDto>>(){new Validator()})
                );

            _mockBizRuleFactory.Setup(mock => mock.Create())
                .Returns
                (
                    new ReadOnlyCollection<IBizRule<CompetitionBizDto>>(new List<IBizRule<CompetitionBizDto>>{new Rule()})
                );

            var result = _facade.StartCompetition(new List<Movie>());
            Assert.NotNull(result);
            Assert.Equal(!validationFailure, result.Ok);

            _mockBizValidationFactory.Verify(mock => mock.Create(), Times.Once);
            _mockBizRuleFactory.Verify(mock => mock.Create(), Times.Exactly(Convert.ToInt32(!validationFailure)));
        }
    }
}