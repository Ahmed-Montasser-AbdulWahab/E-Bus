using E_Bus.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    public class UserDTO
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? NationalID { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public static class ApplicationUserExtensions
    {
        public static UserDTO ToUserDTO(this ApplicationUser user)
        {
            return new UserDTO()
            {
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                NationalID = user.NationalID

            };
        }
    }
}
