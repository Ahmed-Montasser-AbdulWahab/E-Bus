using E_Bus.Entities.DbContext;
using E_Bus.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TranspostationRepository
{
    public class TransportationGetterRepository : IGetterRepository<Transportation>
    {
        private readonly ApplicationDbContext _db;

        private readonly DbSet<Transportation> _transportationTable;

        public TransportationGetterRepository(ApplicationDbContext db)
        {
            _db = db;
            _transportationTable = db.Transportations;
        }

        public async Task<List<Transportation>?> GetAllAsync(bool include = false)
        {
            return await _transportationTable.ToListAsync();
        }

        public async Task<Transportation?> GetByIdAsync(object id, bool include = false)
        {
            return await _transportationTable.FindAsync(id);
        }
    }
}
