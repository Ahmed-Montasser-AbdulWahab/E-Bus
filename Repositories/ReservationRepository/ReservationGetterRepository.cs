using E_Bus.Entities.DbContext;
using E_Bus.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ReservationRepository
{
    public class ReservationGetterRepository : IReservationGetterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Reservation> _reservations;

        public ReservationGetterRepository(ApplicationDbContext context)
        {
            _context = context;
            _reservations = context.Reservations;
        }

        public async Task<List<Reservation>?> GetAllAsync(bool include = false)
        {
            if(include)
            {
                return await _reservations.Include(r => r.User).Include(r => r.Trip).ToListAsync();
            }
            else
            {
                return await _reservations.ToListAsync();
            }
        }

        public async Task<Reservation?> GetByIdAsync(object id, bool include = false)
        {
            List<Reservation> reservations;
            if (include)
            {
                reservations = await _reservations.Include(r => r.User).Include(r => r.Trip).Include(r => r.Trip.ServiceType).ToListAsync();
            }
            else
            {
                reservations = await _reservations.ToListAsync();
            }

            return reservations.FirstOrDefault(r => $"{r.TripId}{r.UserId}".Equals(id));
        }

        public async Task<List<Reservation>?> GetByTripIdAsync(Guid id, bool include = false)
        {
            return (include) ?
                await _reservations.Include(r => r.User).Include(r => r.Trip).Include(r => r.Trip.ServiceType).Where(r => r.TripId == id).OrderByDescending(r => r.ReservationTime).ToListAsync() :
                await _reservations.Where(r => r.TripId == id).OrderByDescending(r => r.ReservationTime).ToListAsync();
        }

        public async Task<List<Reservation>?> GetByUserIdAsync(Guid id, bool include = false)
        {
            return (include) ?
                await _reservations.Include(r => r.User).Include(r => r.Trip).Include(r => r.Trip.ServiceType).Where(r => r.UserId == id).OrderByDescending(r => r.ReservationTime).ToListAsync() :
                await _reservations.Where(r => r.TripId == id).OrderByDescending(r => r.ReservationTime).ToListAsync();
        }
    }
}
