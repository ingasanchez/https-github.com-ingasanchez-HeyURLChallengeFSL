using hey_url_challenge_code_dotnet.Interfaces;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Generics
{
    public class CrudGenerics<T> : IGenerics<T> where T : class
    {
        private ApplicationContext _context;
        private DbSet<T> _eList;

        public CrudGenerics( ApplicationContext context)
        {
            _context = context;
            _eList = context.Set<T>();

        }

        public async Task<bool> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await _eList.AddAsync(item);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _eList.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _eList.FindAsync(id);
        }

        public async Task<bool> Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

                _eList.Remove(item);
            return true;
        }

        public async Task Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
