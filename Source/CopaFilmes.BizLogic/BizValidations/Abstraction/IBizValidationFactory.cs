using System.Collections.Generic;
using FluentValidation;

namespace CopaFilmes.BizLogic.BizValidations.Abstraction
{
    public interface IBizValidationFactory<TDto> where TDto : class
    {
        IReadOnlyCollection<IValidator<TDto>> Create();
    }
}