using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace E_Bus.Entities.Entities
{
    public class Reservation
    {
        public DateTime ReservationTime { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }

        [Required]
        public Guid TripId { get; set; }
        [ForeignKey(nameof(TripId))]
        public virtual Trip? Trip { get; set; }


    }
}
