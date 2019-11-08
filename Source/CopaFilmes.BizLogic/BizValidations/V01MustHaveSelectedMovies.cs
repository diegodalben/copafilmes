using CopaFilmes.BizLogic.BizValidations.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using FluentValidation;

namespace CopaFilmes.BizLogic.BizValidations
{
    public sealed class V01MustHaveSelectedMovies : AbstractValidator<CompetitionBizDto>
    {
        private const int AmountMoviesSelected = 16;

        public V01MustHaveSelectedMovies()
        {
            RuleFor(dto => dto.SelectedMovies.Count)
                .Equal(AmountMoviesSelected)
                .WithMessage($"Deve haver {AmountMoviesSelected} filmes selecionados para a competição.");
        }
    }
}