using hey_url_challenge_code_dotnet.Interfaces;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Transaction
{
    public class Transactions : ITransactions
    {

        public IUrlGenerics UrlGenerics { get; set; }

        public ILogUrlGenerics LogUrlGenerics { get; set; }

        private readonly ApplicationContext _context;
        public Transactions(IUrlGenerics urlGenerics, ILogUrlGenerics logUrlGenerics, ApplicationContext context)
        {
            UrlGenerics = urlGenerics;
            LogUrlGenerics = logUrlGenerics;
            _context = context; 
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
