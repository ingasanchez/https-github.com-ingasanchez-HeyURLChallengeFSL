using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Generics
{
    public class LogUrlGenerics : CrudGenerics<LogUrl>, ILogUrlGenerics
    {
        private IUrlGenerics _urlGenerics;
        public LogUrlGenerics(ApplicationContext context, IUrlGenerics urlGenerics) : base(context)
        {
            _urlGenerics = urlGenerics;
        }

        public async Task<List<LogUrl>> GetLogsUrlByFixUrl(string urlParm)
        {
            var url = await _urlGenerics.GetUrlByFixUrl(urlParm);
            if (url == null)
            {
                throw new Exception(nameof(url));
            }
       
            var logsUrl = GetAll().Result.ToList().Where(i => i.IdUrl == url.IdUrl).ToList();
            return logsUrl;
        }
    }
}
