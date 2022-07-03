using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IFixUrlService _fixUrlService;
        private readonly IUrlGenerics _urlGenerics;
        private readonly ILogUrlGenerics _logUrlGenerics;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IFixUrlService fixUrlService, IUrlGenerics urlGenerics, ILogUrlGenerics logUrlGenerics)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _fixUrlService = fixUrlService;
            _urlGenerics = urlGenerics;
            _logUrlGenerics = logUrlGenerics;
        }

        public async Task <IActionResult> Index()
        {
            var urls = await _fixUrlService.GetAll();
            return View(new HomeViewModel { Urls = urls });
          
        }

        [HttpPost]
        public async Task<IActionResult> Create( HomeViewModel model)
        {
            if (model is not null)
            {
                if (!Uri.IsWellFormedUriString(model.NewUrl.OriginalUrl, UriKind.Absolute))
                {
                    TempData["Notice"] = "URL Specified is not valid.";
                }
                else
                {
                    await this._fixUrlService.CreateFixUrl(model.NewUrl.OriginalUrl);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(model));
            }
            return RedirectToAction("Index");
        }

        [Route("urls/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            try
            {
                await this._fixUrlService.CreateLog(url, browserDetector.Browser.Name, browserDetector.Browser.OS);
                var urlOld = await _urlGenerics.GetUrlByFixUrl(url);
                return Redirect(urlOld.OriginalUrl);
            }catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("urls/Charts/{url}")]
        public async Task<IActionResult> Show(string url) 
        {
            var urlDetails = (List<LogUrl>)await _logUrlGenerics.GetLogsUrlByFixUrl(url);
            var urlPri = (Url)await _urlGenerics.GetUrlByFixUrl(url);

            Dictionary<string, int> DailyClicks = new Dictionary<string, int>();
            Dictionary<string, int> Platform = new Dictionary<string, int>();
            Dictionary<string, int> Browser = new Dictionary<string, int>();


            var lstOS = from r in urlDetails
                        orderby r.UserPlatform
                        group r by r.UserPlatform into plf
                        select new { key = plf.Key, plat = plf.Count() };

            var lstBr = from r in urlDetails
                        orderby r.Browser
                        group r by r.Browser into brs
                        select new { key = brs.Key, plat = brs.Count() };

            var lstDay = from r in urlDetails
                        where r.LogDate.Month == DateTime.UtcNow.Month
                        orderby r.LogDate.Day
                        group r by r.LogDate.Day into dlf
                        select new { key = dlf.Key, plat = dlf.Count() };

            foreach (var item in lstOS)
            {
                Platform.Add(item.key, item.plat);
            }
            foreach (var item in lstBr)
            {
                Browser.Add(item.key, item.plat);
            }
            foreach (var item in lstDay)
            {
                DailyClicks.Add(item.key.ToString(), item.plat);
            }

            return View(new ShowViewModel
            {
                Url = urlPri,
                DailyClicks = DailyClicks,
                BrowseClicks = Browser,
                PlatformClicks = Platform
            });
        }
    }
}