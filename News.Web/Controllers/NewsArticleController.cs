using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Entity;
using News.Web.Interfaces;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Web.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly IApiClient _apiClient;

        public NewsArticleController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var responseMessage = await _apiClient.GetAsync("news");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();

                var newsArticles = JsonConvert.DeserializeObject<List<NewsArticle>>(responseData);

                return View(newsArticles);
            }
            return View("Error");
        }
    }
}
