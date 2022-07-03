using hey_url_challenge_code_dotnet.Models;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Interfaces
{
    public interface IUrlGenerics : IGenerics<Url>
    {
        Task<Url> GetUrlByFixUrl(string urlParm);
    }
}
