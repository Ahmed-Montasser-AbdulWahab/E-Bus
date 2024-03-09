using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ReservationDTO
{
    public class ReservationId
    {
        [Required(ErrorMessage = "{0} field is required.")]
        public Guid TripId { get; set; }
        [Required(ErrorMessage = "{0} field is required.")]
        public Guid? UserId { get; set; }
    }
}
