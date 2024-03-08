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

    public class TripAdderRepository : IAdderRepository<Trip>
    {
        private readonly ApplicationDbContext _db;

        private readonly DbSet<Trip> _tripTable;

        public TripAdderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _tripTable = db.Trips;

        }

        public async Task<bool> AddAsync(Trip trip)
        {
            _tripTable.Add(trip);

            return (await _db.SaveChangesAsync()) > 0;


        }
    }
}
