using System.Threading.Tasks;
using CopaFilmes.Infrastructure.HttpClient.Abstraction;

namespace CopaFilmes.Infrastructure.HttpClient
{
    public sealed class HttpClientHandler : IHttpHandler
    {
        private readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

        public Task<string> GetStringAsync(string requestUri)
        {
            return _client.GetStringAsync(requestUri);
        }
    }
}