using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IAdderRepository<T> where T : class
    {
        public Task<bool> AddAsync(T item);
    }
}
