using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUpdaterRepository<T> where T : class
    {
        public Task<bool> UpdateAsync(T value);
    }
}
