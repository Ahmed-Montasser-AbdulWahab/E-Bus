using AutoMapper;
using E_Bus.Entities.DbContext;
using E_Bus.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TripRepository
{
    public class TripGetterRepository : IGetterRepository<Trip>
    {
        private readonly ApplicationDbContext _db;

        private readonly DbSet<Trip> _tripTable;

        public TripGetterRepository(ApplicationDbContext db)
        {
            _db = db;
            _tripTable = db.Trips;
        }

        public async Task<List<Trip>?> GetAllAsync(bool include = false)
        {
            if(include)
            {
                return await _tripTable.Include(t => t.ServiceType).Include(t => t.Reservations).ToListAsync();
            }
            else
            {
                return await _tripTable.ToListAsync();
            }
        }

        public async Task<Trip?> GetByIdAsync(object id, bool include = false)
        {
            if (include)
            {
                return await _tripTable.Include(t => t.ServiceType).Include(t => t.Reservations).FirstOrDefaultAsync(t => t.Id == (Guid) id);
            }
            else
            {
                return await _tripTable.FindAsync(id);
            }
        }
    }
}
