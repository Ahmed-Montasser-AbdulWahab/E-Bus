using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Bus.Entities.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public virtual DbSet<Trip> Trips { get; set; }

        public virtual DbSet<Transportation> Transportations { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Reservation>().HasKey(nameof(Reservation.UserId), nameof(Reservation.TripId));  
        }
    }
}
