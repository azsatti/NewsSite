using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.DataAccess;
using News.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.API.Controllers
{
    /// <summary>
    /// News controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NewsController : Controller
    {
        private readonly IDataRepository<NewsArticle, long> _repo;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        public NewsController(IDataRepository<NewsArticle, long> repo, ILogger<NewsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// Gets All news
        /// </summary>
        /// <remarks>// GET: api/news</remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<NewsArticle>> GetAll()
        {
            return await _repo.GetAll();
        }

        /// <summary>
        /// Get News By Id
        /// </summary>
        /// <remarks>
        /// // GET api/news/5
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "GetNewsById")]
        public async Task<IActionResult> GetById(long id)
        {
            var newsArticle = await _repo.Get(id);
            if (newsArticle == null)
            {
                _logger.LogWarning($"No record found for news article:{id}");
                return NotFound();
            }

            return Ok(newsArticle);
        }

        /// <summary>
        /// Adds a new article to news list
        /// </summary>
        /// <param name="newsArticle"></param>
        /// <returns>Returns the newly created news artile</returns>
        /// <response code="201">Returns the newly created news artile</response>
        /// <response code="400">If the news article is null</response>  
        [HttpPost(Name = "AddNewsArticle")]
        [ProducesResponseType(typeof(NewsArticle), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody]NewsArticle newsArticle)
        {
            if (newsArticle == null)
            {
                _logger.LogError("Bad request");
                return BadRequest();
            }

            await _repo.Add(newsArticle);

            return CreatedAtRoute("GetNewsById",
                new {id = newsArticle.Id},
                newsArticle);
        }

        /// <summary>
        /// Updates existing news article
        /// </summary>
        /// <remarks>PUT api/news/5</remarks>
        /// <param name="id"></param>
        /// <param name="newsArticle"></param>
        /// <returns></returns>
        [HttpPut("{id:long}", Name = "UpdateNewsArticle")]
        public async Task<IActionResult> Put(long id, [FromBody]NewsArticle newsArticle)
        {
            var isSuccess = await _repo.Update(id,
                newsArticle);

            if (!isSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete an existing news article
        /// </summary>
        /// <remarks>DELETE api/news/5</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}", Name = "DeleteNewsArticle")]
        public async Task<IActionResult> Delete(long id)
        {
            var isSuccess = await _repo.Delete(id);

            if (!isSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
