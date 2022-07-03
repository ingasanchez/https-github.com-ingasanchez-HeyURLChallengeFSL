using hey_url_challenge_code_dotnet.Interfaces;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Transaction
{
    public interface ITransactions
    {
        IUrlGenerics UrlGenerics { get; }
        ILogUrlGenerics LogUrlGenerics { get; }

        Task Save();

    }
}
