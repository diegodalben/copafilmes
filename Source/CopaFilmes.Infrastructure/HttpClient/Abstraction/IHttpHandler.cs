using System.Threading.Tasks;

namespace CopaFilmes.Infrastructure.HttpClient.Abstraction
{
    public interface IHttpHandler
    {
        Task<string> GetStringAsync(string requestUri);
    }
}