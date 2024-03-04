using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Bus.Entities.Entities
{
    public class Trip
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(40)]
        public string? Starting {  get; set; }

        [Required]
        [StringLength(40)]
        public string? Ending { get; set;}

        
        public Guid ServiceTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(ServiceTypeId))]
        public virtual TransportationService? ServiceType { get; set; }

        [Required]
        public int? Ticket { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
