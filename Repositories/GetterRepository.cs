using E_Bus.Entities.DbContext;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GetterRepository<T> : IGetterRepository<T> where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<T> repositorySet;

        public GetterRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            repositorySet = applicationDbContext.Set<T>();
        }

        public async Task<List<T>?> GetAllAsync()
        {
            return await repositorySet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await repositorySet.FindAsync(id);
        }

    }
}
