using hey_url_challenge_code_dotnet.Services;
using NUnit.Framework;
using HeyUrlChallengeCodeDotnet.Controllers;
using hey_url_challenge_code_dotnet.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.ViewModels;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using System.Linq;
using System;

namespace tests
{
    public class UrlsControllerTest
    {

        private Mock<IUrlGenerics> _mockUrlGenerics;
        private Mock<ILogUrlGenerics> _mockLogUrlGenerics;
        private Mock<ILogger<UrlsController>> _mockUrlController;
        private Mock<IBrowserDetector> _mockBrowser;
        private Mock<IFixUrlService> _mockFixUrlServ;

        [SetUp]
        public void Setup()
        {
            _mockFixUrlServ = new Mock<IFixUrlService>();
            _mockBrowser = new Mock<IBrowserDetector>();
            _mockUrlController = new Mock<ILogger<UrlsController>>();
            _mockUrlGenerics = new Mock<IUrlGenerics>();
            _mockLogUrlGenerics = new Mock<ILogUrlGenerics>();
        }

        [Test]
        public async Task TestUrlIndexNotNullReturn()
        {
            _mockFixUrlServ.Setup(u => u.GetAll())
                .ReturnsAsync(new List<Url>
                {
                    new Url{ OriginalUrl = "www.google.com",
                             ShortUrl = "ABCCD" }
                });
            var obj = new UrlsController(_mockUrlController.Object, _mockBrowser.Object, _mockFixUrlServ.Object, _mockUrlGenerics.Object, _mockLogUrlGenerics.Object);
            var result = await obj.Index() as ViewResult;
            var model = result.Model as HomeViewModel;

            Assert.NotNull(model);
            Assert.AreEqual(1, model.Urls.Count());
            Assert.AreNotEqual("ABCDD", model.Urls.First().ShortUrl);

        }
        [Test]
        public Task TestCreateModelNullWhenArgumentNullException()
        {
            var obj = new UrlsController(_mockUrlController.Object, _mockBrowser.Object, _mockFixUrlServ.Object, _mockUrlGenerics.Object, _mockLogUrlGenerics.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                var result = await obj.Create(null);
            });
            return Task.CompletedTask;
        }


    }
}