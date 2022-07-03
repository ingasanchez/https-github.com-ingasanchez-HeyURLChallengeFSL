using hey_url_challenge_code_dotnet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UrlRestController : Controller
    {
        private IUrlGenerics _urlGenerics;
        public UrlRestController(IUrlGenerics urlGenerics)
        {
            _urlGenerics = urlGenerics;
        }


        [HttpGet("Get10LastUrl")]
        public IActionResult Get10LastURL()
        {
            try
            {
                var ls = _urlGenerics.GetAll().Result.ToList().Take(10).OrderByDescending(x => x.UrlDate);
                return Ok(ls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
