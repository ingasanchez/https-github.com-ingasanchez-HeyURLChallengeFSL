using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Generics
{
    public class UrlGenerics : CrudGenerics<Url>, IUrlGenerics
    {
        public UrlGenerics(ApplicationContext context) : base(context)
        {
        }

        public async Task<Url> GetUrlByFixUrl(string urlParm)
        {
            Url url = null;
            url = (Url)GetAll().Result.ToList().Where(i => i.ShortUrl.Equals(urlParm)).FirstOrDefault();
            return url;
        }
    }
}
