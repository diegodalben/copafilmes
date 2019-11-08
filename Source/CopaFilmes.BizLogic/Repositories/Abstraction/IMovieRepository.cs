using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.Repositories.Abstraction
{
    public interface IMovieRepository
    {
        IList<Movie> GetMovies();
    }
}