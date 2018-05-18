using News.Entity;

namespace News.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }

        public DbSet<NewsArticle> NewsArticles { get; set; }
    }
}
