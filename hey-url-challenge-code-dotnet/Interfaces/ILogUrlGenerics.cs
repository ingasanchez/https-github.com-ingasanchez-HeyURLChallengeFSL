using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Interfaces
{
    public interface ILogUrlGenerics : IGenerics<LogUrl>
    {
        public Task<List<LogUrl>> GetLogsUrlByFixUrl(string urlParm);
    }
}
