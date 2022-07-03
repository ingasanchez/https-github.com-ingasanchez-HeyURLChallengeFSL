using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Interfaces
{
    public interface IGenerics <T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> Add (T item);

        public Task<bool> Remove (T item);

        public Task<T> GetById(int id);

        public Task Update(T item);
    }
}
