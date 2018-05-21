using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Entity;
using News.Web.Interfaces;
using Newtonsoft.Json;

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

        public IActionResult Create()
        {
            return View();
        }

        // POST: news/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,DatePublished,AuthorName")]
                                                NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(newsArticle),
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage res = await _apiClient.PostAsync("news",
                    content);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("Error");
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dto = new List<NewsArticle>();

            HttpResponseMessage res = await _apiClient.GetAsync("news");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<NewsArticle>>(result);
            }

            var newsArticle = dto.SingleOrDefault(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: news/Edit/1  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
                                              [Bind("Id,Title,Body,DatePublished,AuthorName")]
                                              NewsArticle newsArticle)
        {
            if (id != newsArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var content = new StringContent(JsonConvert.SerializeObject(newsArticle),
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage res = await _apiClient.PutAsync($"news/{id}",
                    content);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(newsArticle);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dto = new List<NewsArticle>();
            HttpResponseMessage res = await _apiClient.GetAsync("news");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<NewsArticle>>(result);
            }

            var newsArticle = dto.SingleOrDefault(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: news/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var res = await _apiClient.DeleteAsync($"news/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

    }
}
