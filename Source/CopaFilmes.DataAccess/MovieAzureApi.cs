using System;
using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;
using CopaFilmes.BizLogic.Repositories.Abstraction;
using CopaFilmes.Infrastructure.HttpClient.Abstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CopaFilmes.DataAccess
{
    public sealed class MovieAzureApi : IMovieRepository
    {
        private readonly IHttpHandler _httpHandler;
        private readonly DataAccessSettings _settings;

        public MovieAzureApi
        (
            IHttpHandler httpHandler,
            IOptions<DataAccessSettings> options
        )
        {
            _httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public IList<Movie> GetMovies()
        {
            var response = _httpHandler.GetStringAsync(_settings.UrlMovieEndpoint).Result;
            return JsonConvert.DeserializeObject<IList<Movie>>(response);
        }
    }
}