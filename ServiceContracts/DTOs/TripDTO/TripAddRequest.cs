using E_Bus.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.CustomValidationAttributes;

namespace ServiceContracts.DTOs.TripDTO
{
    public class TripAddRequest
    {

        [Required(ErrorMessage = "{0} Terminal name must be supplied.")]
        [StringLength(40, ErrorMessage = "{0} Terminal Name must not exceed 40 characters.")]
        public string? Starting { get; set; }

        [Required(ErrorMessage = "{0} Terminal name must be supplied.")]
        [StringLength(40, ErrorMessage = "{0} Terminal Name must not exceed 40 characters.")]
        public string? Ending { get; set; }

        [Required(ErrorMessage = "{0} field name must be supplied.")]
        public Guid ServiceTypeId { get; set; }


        [Required(ErrorMessage = "{0} field name must be supplied.")]
        [DataType(DataType.Currency)]
        public int Ticket { get; set; }

        [Required(ErrorMessage = "{0} field name must be supplied.")]
        [DepartureArrivalCheck("ArrivalTime")]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "{0} field name must be supplied.")]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        public Trip ToTrip()
        {
            return new Trip()
            {
                Id = Guid.NewGuid(),
                ArrivalTime = ArrivalTime,
                DepartureTime = DepartureTime,
                ServiceTypeId = ServiceTypeId,
                Ticket = Ticket,
                Ending = Ending,
                Starting = Starting
            };
        }
    }
}
