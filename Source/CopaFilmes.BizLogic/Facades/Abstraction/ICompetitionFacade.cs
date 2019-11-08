using System.Collections.Generic;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.Facades.Abstraction
{
    public interface ICompetitionFacade
    {
        ResponseBag<IList<Movie>> GetMovies();
        ResponseBag<CompetitionBizDto> StartCompetition(IList<Movie> selectedMovies);
    }
}