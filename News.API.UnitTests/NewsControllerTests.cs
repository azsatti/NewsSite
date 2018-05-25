using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetList());
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(1, result.FirstOrDefault().Id);
        }

        [Test]
        public async Task GetById_Should_ReturnNewsArticle()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();

            var newsArticle = new NewsArticle
            {
                Id = 1,
                AuthorName = "Author",
                Body = "Test1",
                DatePublished = DateTime.UtcNow,
                Title = "Title"
            };

            mockRepo.Setup(repo => repo.Get(1)).ReturnsAsync(newsArticle).Verifiable();
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.GetById(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(NewsArticle), result.Value);
        }

        [Test]
        public async Task GetById_Should_ReturnNotFound()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();

            mockRepo.Setup(repo => repo.Get(2)).ReturnsAsync((NewsArticle)null).Verifiable();
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.GetById(2) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public async Task Add_Should_ReturnBadRequest()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();

            mockRepo.Setup(repo => repo.Add(null)).Verifiable();
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.Create(null) as BadRequestResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task Add_Should_ReturnNewsArticle()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();
            var newsArticle = new NewsArticle
            {
                Id = 1,
                AuthorName = "Author",
                Body = "Test1",
                DatePublished = DateTime.UtcNow,
                Title = "Title"
            };

            mockRepo.Setup(repo => repo.Add(newsArticle)).Returns(Task.FromResult(GetNewsArticle())).Verifiable();
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.Create(newsArticle) as CreatedAtRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
            mockRepo.Verify();

            var returnedValue = (NewsArticle)result.Value;
            Assert.AreEqual(1, returnedValue.Id);
            Assert.AreEqual("Title", returnedValue.Title);
        }

        [Test]
        public async Task Delete_Should_RemoveItemfromList()
        {
            var mockRepo = new Mock<IDataRepository<NewsArticle, long>>(MockBehavior.Strict);
            var mocklogger = new Mock<ILogger<NewsController>>();

            mockRepo.Setup(repo => repo.Delete(1)).ReturnsAsync(true);
            var controller = new NewsController(mockRepo.Object, mocklogger.Object);

            var result = await controller.Delete(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        private List<NewsArticle> GetList()
        {
           return new List<NewsArticle>
           {
               new NewsArticle {Id = 1, AuthorName = "Author", Body = "Test1", DatePublished = DateTime.UtcNow, Title = "Title"}
           };
        }

        private NewsArticle GetNewsArticle()
        {
            return new NewsArticle
            {
                Id = 1,
                AuthorName = "Author",
                Body = "Test1",
                DatePublished = DateTime.UtcNow,
                Title = "Title"
            };
        }
    }
}
