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
    public class TripUpdaterRepository : IUpdaterRepository<Trip>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Trip> _tripTable;

        public TripUpdaterRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _tripTable = db.Trips;
            _mapper = mapper;
        }

        public async Task<bool> UpdateAsync(Trip value)
        {
            var tripFromDb = await _tripTable.FindAsync(value.Id);

            if(tripFromDb is null) { return false; }

            tripFromDb = _mapper.Map<Trip,Trip>(value, tripFromDb);

            return (await _db.SaveChangesAsync()) > 0;


        }
    }
}
