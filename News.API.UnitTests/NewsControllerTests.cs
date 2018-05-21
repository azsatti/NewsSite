using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using News.API.Controllers;
using News.DataAccess;
using News.Entity;
using NUnit.Framework;

namespace News.API.UnitTests
{
    [TestFixture]
    public class NewsControllerTests
    {
        [Test]
        public async Task GetAll_Should_ReturnListOfNewsArticle()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>();
            var mocklogger = new Mock<ILogger<NewsController>>();

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetList());
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(1, result.FirstOrDefault().Id);
        }

        private List<NewsArticle> GetList()
        {
           return new List<NewsArticle>
           {
               new NewsArticle {Id = 1, AuthorName = "Author", Body = "Test1", DatePublished = DateTime.UtcNow, Title = "Title"}
           };
        }
    }
}
