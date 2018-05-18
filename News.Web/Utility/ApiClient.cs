using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using News.Web.Interfaces;

namespace News.Web.Utility
{
    public class ApiClient : IApiClient
    {
        readonly HttpClient _client = new HttpClient();

        public ApiClient(IConfiguration configuration)
        {
            var apiUrl = configuration["APIURL"];

            _client.BaseAddress = new Uri(apiUrl);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}
