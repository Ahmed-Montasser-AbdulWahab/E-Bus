using ServiceContracts.DTOs.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    public class UserTripWrapperModel
    {
        public UserDTO? TheUserDTO {get; set;}

        public TripResponse? TheTripResponse { get; set;}

    }
}
