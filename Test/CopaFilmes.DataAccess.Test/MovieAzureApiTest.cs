using System;
using Xunit;
using Moq;
using CopaFilmes.Infrastructure.HttpClient.Abstraction;
using System.Linq;
using Microsoft.Extensions.Options;

namespace CopaFilmes.DataAccess.Test
{
    public class MovieAzureApiTest
    {
        private readonly Mock<IHttpHandler> _mockHttpHandler;
        private readonly Mock<IOptions<DataAccessSettings>> _mockOptions;
        private readonly MovieAzureApi _repository;

        public MovieAzureApiTest()
        {
            _mockHttpHandler = new Mock<IHttpHandler>();
            _mockOptions = new Mock<IOptions<DataAccessSettings>>();
            _mockOptions.Setup(mock => mock.Value).Returns(new DataAccessSettings());

            _repository = new MovieAzureApi(_mockHttpHandler.Object, _mockOptions.Object);
        }

        [Fact]
        public void should_return_exception_constructor_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => new MovieAzureApi(null, _mockOptions.Object));
            Assert.Throws<ArgumentNullException>(() => new MovieAzureApi(_mockHttpHandler.Object, null));
        }

        [Fact]
        public void should_get_movies()
        {
            _mockHttpHandler
                .Setup(mock => mock.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("[{id:\"a1\", primaryTitle:\"title\"}]");

            var result = _repository.GetMovies();
            Assert.NotNull(result);
            Assert.True(result.Any());

            _mockHttpHandler.Verify(mock => mock.GetStringAsync(It.IsAny<string>()), Times.Once);
        }
    }
}