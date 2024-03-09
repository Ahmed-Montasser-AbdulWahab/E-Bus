using E_Bus.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ReservationDTO
{
    public class ReservationAddRequest
    {
        [Required(ErrorMessage = "{0} field is required.")]
        public DateTime ReservationTime { get; set; }

        [Required(ErrorMessage = "{0} field is required.")]
        public Guid UserId { get; set; }


        [Required(ErrorMessage = "{0} field is required.")]
        public Guid TripId { get; set; }
    
        public Reservation ToReservation()
        {
            return new Reservation
            {
                ReservationTime = ReservationTime,
                UserId = UserId,
                TripId = TripId
            };
        }
    }

    
}
