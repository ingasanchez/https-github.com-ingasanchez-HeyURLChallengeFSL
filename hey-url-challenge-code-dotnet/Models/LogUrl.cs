using System;

namespace hey_url_challenge_code_dotnet.Models
{
    public class LogUrl
    {
        public int IdLog { get; set; }
        public int IdUrl { get; set; }
        public string UserPlatform { get; set; }

        public string Browser { get; set; }

        public DateTime LogDate { get; set; }
    }
}
