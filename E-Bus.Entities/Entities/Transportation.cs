using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Bus.Entities.Entities
{
    public class Transportation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public bool IsAirConditioned { get; set; }
    }
}
