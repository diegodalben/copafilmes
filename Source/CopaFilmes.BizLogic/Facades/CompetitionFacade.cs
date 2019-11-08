using System;
using System.Collections.Generic;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizValidations.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Facades.Abstraction;
using CopaFilmes.BizLogic.Repositories.Abstraction;

namespace CopaFilmes.BizLogic.Facades
{
    public sealed class CompetitionFacade : ICompetitionFacade
    {
        private readonly IBizValidationFactory<CompetitionBizDto> _bizValidationFactory;
        private readonly IBizRuleFactory<CompetitionBizDto> _bizRuleFactory;
        private readonly IMovieRepository _movieRepository;

        public CompetitionFacade
        (
            IBizValidationFactory<CompetitionBizDto> bizValidationFactory,
            IBizRuleFactory<CompetitionBizDto> bizRuleFactory,
            IMovieRepository movieRepository
        )
        {
            _bizValidationFactory = bizValidationFactory ?? throw new ArgumentNullException(nameof(bizValidationFactory));
            _bizRuleFactory = bizRuleFactory ?? throw new ArgumentNullException(nameof(bizRuleFactory));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public ResponseBag<IList<Movie>> GetMovies()
        {
            return new ResponseBag<IList<Movie>>
            {
                Ok = true,
                ObjectResponse = _movieRepository.GetMovies()
            };
        }

        public ResponseBag<CompetitionBizDto> StartCompetition(IList<Movie> selectedMovies)
        {
            var dto = new CompetitionBizDto{SelectedMovies = selectedMovies};

            var validations = _bizValidationFactory.Create();
            foreach (var validation in validations)
            {
                var result = validation.Validate(dto);
                if(!result.IsValid)
                {
                    return new ResponseBag<CompetitionBizDto>
                    {
                        Ok = false,
                        Message = string.Join(", ", result.Errors)
                    };
                }
            }

            var rules = _bizRuleFactory.Create();
            foreach (var rule in rules)
            {
                rule.Execute(dto);
            }

            return new ResponseBag<CompetitionBizDto>
            {
                Ok = true,
                ObjectResponse = dto
            };
        }
    }
}