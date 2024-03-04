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
    public class AdderRepository<T> : IAdderRepository<T> where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<T> repositorySet;

        public AdderRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            repositorySet = applicationDbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T item)
        {
            repositorySet.Add(item);
            return (await applicationDbContext.SaveChangesAsync() > 0);
        }
    }
}
