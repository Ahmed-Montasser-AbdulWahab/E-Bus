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

namespace Repositories.ReservationRepository
{
    public class ReservationUpdaterRepository : IUpdaterRepository<Reservation>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Reservation> _reservations;
        private readonly IMapper _mapper;

        public ReservationUpdaterRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _reservations = context.Reservations;
            _mapper = mapper;
        }

        public async Task<bool> UpdateAsync(Reservation value)
        {
            Reservation? reservationFromDb = await _reservations.FirstOrDefaultAsync(r => r.UserId == value.UserId && r.TripId == value.TripId);
            if (reservationFromDb is null) { return false; }
            reservationFromDb = _mapper.Map(value, reservationFromDb);

            return (await _context.SaveChangesAsync()) > 0;
         }
    }
}
