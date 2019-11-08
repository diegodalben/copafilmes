using System.Collections.Generic;
using System.Collections.ObjectModel;
using CopaFilmes.BizLogic.BizValidations.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using FluentValidation;

namespace CopaFilmes.BizLogic.BizValidations
{
    public class CompetitionBizValidationFactory : IBizValidationFactory<CompetitionBizDto>
    {
        public IReadOnlyCollection<IValidator<CompetitionBizDto>> Create()
        {
            var validations = new List<IValidator<CompetitionBizDto>>
            {
                new V01MustHaveSelectedMovies()
            };

            return new ReadOnlyCollection<IValidator<CompetitionBizDto>>(validations);
        }
    }
}