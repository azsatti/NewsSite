using System.Net.Http;
using System.Threading.Tasks;

namespace News.Web.Interfaces
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
