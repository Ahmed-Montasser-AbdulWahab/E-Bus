using E_Bus.Entities.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UpdaterRepository<T> : IUpdaterRepository<T> where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<T> repositorySet;

        public UpdaterRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            repositorySet = applicationDbContext.Set<T>();
        }

        public async Task<bool> UpdateAsync(T value)
        {
            repositorySet.Attach(value);
            //Then set the state of the Entity as Modified
            repositorySet.Entry(value).State = EntityState.Modified;

            return (await applicationDbContext.SaveChangesAsync() > 0);
        }
    }
}
