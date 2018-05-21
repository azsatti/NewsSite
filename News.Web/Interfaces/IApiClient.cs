using System.Net.Http;
using System.Threading.Tasks;

namespace News.Web.Interfaces
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetAsync(string url);

        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);

        Task<HttpResponseMessage> DeleteAsync(string url);
    }
}
