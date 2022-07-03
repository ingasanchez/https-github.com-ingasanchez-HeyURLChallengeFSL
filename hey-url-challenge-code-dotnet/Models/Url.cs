using System;
namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public int IdUrl { get; set; }
        public string ShortUrl { get; set; }

        public string OriginalUrl { get; set; }
        public int CountAccess { get; set; }
        public DateTime UrlDate { get; set; }
    }
}
