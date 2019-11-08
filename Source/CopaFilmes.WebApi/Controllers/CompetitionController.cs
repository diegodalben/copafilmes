using System.Collections.Generic;
using System.Linq;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Facades.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly ICompetitionFacade _facade;

        public CompetitionController(ICompetitionFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var result = _facade.GetMovies();
            if(!result.ObjectResponse.Any())
            {
                return NotFound();
            }

            return new OkObjectResult(result.ObjectResponse);
        }

        [HttpPost]
        public IActionResult StartCompetition([FromBody] IList<Movie> movies)
        {
            var result = _facade.StartCompetition(movies);
            if(!result.Ok)
            {
                return BadRequest(new {Message = result.Message});
            }

            return new OkObjectResult(result.ObjectResponse);
        }
    }
}