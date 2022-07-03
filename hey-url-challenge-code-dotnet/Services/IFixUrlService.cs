using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public interface IFixUrlService
    {
        Task CreateFixUrl(string originalUrl);
        Task<IEnumerable<Url>> GetAll();

        Task CreateLog(string IdUrl, string browser, string platform);
    }
}
