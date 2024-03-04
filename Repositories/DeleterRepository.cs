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
    public class DeleterRepository<T> : IDeleterRepository<T> where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<T> repositorySet;

        public DeleterRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            repositorySet = applicationDbContext.Set<T>();
        }

        public async Task<bool> DeleteAsync(object entityId)
        {
            var entity = await repositorySet.FindAsync(entityId);
            if (entity is null)
            {
                return false;
            }

            repositorySet.Remove(entity);
            return (await applicationDbContext.SaveChangesAsync() > 0) ;
        }
    }
}
