using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Entity;

namespace News.DataAccess.News.Repository
{
    public class NewsRepo : IDataRepository<NewsArticle, long>
    {
        private readonly NewsDbContext _context;

        public NewsRepo(NewsDbContext context)
        {
            _context = context;

            if (!_context.NewsArticles.Any())
            {
                _context.Set<NewsArticle>().Add(new NewsArticle
                {
                    Id = 1,
                    Title = "News1",
                    Body = "News1-Body",
                    DatePublished = DateTime.UtcNow,
                    AuthorName = "Author"
                });

                _context.SaveChanges();
            }
        }

        public async Task<List<NewsArticle>> GetAll()
        {
            return await _context.Set<NewsArticle>().ToListAsync();
        }

        public async Task<NewsArticle> Get(long id)
        {
            return await _context.Set<NewsArticle>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Add(NewsArticle newsArticle)
        {
            if (newsArticle == null)
            {
                throw new ArgumentNullException();
            }

            await _context.Set<NewsArticle>().AddAsync(newsArticle);
            _context.SaveChanges();
        }

        public async Task<bool> Update(long id, NewsArticle newsArticle)
        {
            var existingNewsArticle = await _context.Set<NewsArticle>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (existingNewsArticle == null)
            {
                return false;
            }

            existingNewsArticle.Title = newsArticle.Title;
            existingNewsArticle.Body = newsArticle.Body;
            existingNewsArticle.DatePublished = newsArticle.DatePublished;
            existingNewsArticle.AuthorName = newsArticle.AuthorName;

            _context.NewsArticles.Update(existingNewsArticle);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(long id)
        {
            var existingNewsArticle = await _context.Set<NewsArticle>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (existingNewsArticle == null)
            {
                return false;
            }

            _context.NewsArticles.Remove(existingNewsArticle);
            _context.SaveChanges();
            return true;
        }
    }
}
