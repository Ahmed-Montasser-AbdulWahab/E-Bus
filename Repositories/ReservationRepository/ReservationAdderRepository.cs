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
    public class ReservationAdderRepository : IAdderRepository<Reservation>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Reservation> _reservations;

        public ReservationAdderRepository(ApplicationDbContext context)
        {
            _context = context;
            _reservations = context.Reservations;
        }

        public async Task<bool> AddAsync(Reservation reservation)
        {
            _reservations.Add(reservation);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
