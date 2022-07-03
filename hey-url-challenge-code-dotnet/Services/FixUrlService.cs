using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public class FixUrlService : IFixUrlService
    {
        private ITransactions _transactions;
        private IUrlGenerics _urlGenerics;
        private ILogUrlGenerics _logUrlGenerics;

        public FixUrlService( ITransactions transactions, IUrlGenerics urlGenerics, ILogUrlGenerics logUrlGenerics)
        {
            _transactions = transactions;
            _urlGenerics = urlGenerics;
            _logUrlGenerics = logUrlGenerics;
        }

        public async Task CreateFixUrl(string originalUrl)
        {
            if (originalUrl == null)
                throw new ArgumentNullException(nameof(originalUrl));

            // Verify if new Url Exists

            var newUrl = GetFixUrl();
            while (await _urlGenerics.GetUrlByFixUrl(newUrl) is not null)
            {
                newUrl = GetFixUrl();
            }

            Url url = new Url
            {
                CountAccess = 0,
                OriginalUrl = originalUrl,
                ShortUrl = newUrl,
                UrlDate = DateTime.UtcNow
            };
            await _transactions.UrlGenerics.Add(url);
            await _transactions.Save();
        }

        private static string GetFixUrl()
        {
            List<char> characters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            string Url = "";
            Random rand = new Random();

            for (int i=0; i < 5; i++)
            {
                int random = 0;
                random = rand.Next(characters.Count);
                Url += characters[random].ToString();
            }

            return Url;
        }

        public async Task<IEnumerable<Url>> GetAll()
        {
            return await _transactions.UrlGenerics.GetAll();
        }

        public async Task CreateLog(string IdUrl, string browser, string platform)
        {
            var url = await _urlGenerics.GetUrlByFixUrl(IdUrl);
            if (url == null)
            {
                throw new Exception($"Url with ID {IdUrl} doesn't exist.");
            }

            var logUrl = new LogUrl
            {
                Browser = browser,
                UserPlatform = platform,
                LogDate = DateTime.UtcNow,
                IdUrl = url.IdUrl
            };

            url.CountAccess++;
            await _transactions.LogUrlGenerics.Add(logUrl);
            await _transactions.UrlGenerics.Update(url);
            await _transactions.Save();
        }


    }
}
