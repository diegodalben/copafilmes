using System.Collections.Generic;
using AutoFixture.Xunit2;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Facades.Abstraction;
using CopaFilmes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CopaFilmes.WebApi.Test.Controllers
{
    public class CompetitionControllerTest
    {
        private readonly Mock<ICompetitionFacade> _mockFacade;
        private readonly CompetitionController _controller;

        public CompetitionControllerTest()
        {
            _mockFacade = new Mock<ICompetitionFacade>();
            _controller = new CompetitionController(_mockFacade.Object);
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void get_movies_test(bool isMoviesFound)
        {
            _mockFacade.Setup(mock => mock.GetMovies())
                .Returns
                (
                    new ResponseBag<IList<Movie>>
                    {
                        ObjectResponse = isMoviesFound 
                        ? new List<Movie>{new Movie()} 
                        : new List<Movie>()
                    }
                );

            var response = _controller.GetMovies();
            if(isMoviesFound)
                Assert.IsType<OkObjectResult>(response);
            else
                Assert.IsType<NotFoundResult>(response);

            _mockFacade.Verify(mock => mock.GetMovies(), Times.Once);
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void start_competition_test(bool isOk)
        {
            _mockFacade.Setup(mock => mock.StartCompetition(It.IsAny<IList<Movie>>()))
                .Returns(new ResponseBag<CompetitionBizDto>{Ok = isOk});

            var response = _controller.StartCompetition(new List<Movie>());
            if(isOk)
                Assert.IsType<OkObjectResult>(response);
            else
                Assert.IsType<BadRequestObjectResult>(response);

            _mockFacade.Verify(mock => mock.StartCompetition(It.IsAny<IList<Movie>>()), Times.Once);
        }
    }
}