using AutoFixture.Xunit2;
using CopaFilmes.BizLogic.BizValidations;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using Xunit;

namespace CopaFilmes.BizLogic.Test.BizValidations
{
    public class V01MustHaveSelectedMoviesTest
    {
        private readonly V01MustHaveSelectedMovies _validation = new V01MustHaveSelectedMovies();

        [Theory]
        [InlineAutoData(0, false)]
        [InlineAutoData(15, false)]
        [InlineAutoData(16, true)]
        [InlineAutoData(17, false)]
        public void validation_test(int amount, bool isValid)
        {
            var dto = new CompetitionBizDto();
            for (int i = 0; i < amount; i++)
            {
                dto.SelectedMovies.Add(new Movie());
            }

            var result = _validation.Validate(dto);
            Assert.NotNull(result);
            Assert.Equal(isValid, result.IsValid);
        }
    }
}