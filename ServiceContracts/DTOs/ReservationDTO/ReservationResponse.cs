using E_Bus.Entities.Entities;
using ServiceContracts.DTOs.ReservationDTO;
using ServiceContracts.DTOs.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ReservationDTO
{
    public class ReservationResponse
    {
        public UserDTO? TheUserDTO { get; set; }

        public TripResponse? TheTripResponse { get; set; }

        public string? ReservationTime { get; set; }



    }

    public static class ReservationExtension
    {
        public static ReservationResponse ToResponse(this Reservation reservation)
        {
            return new ReservationResponse()
            {
                ReservationTime = reservation.ReservationTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                TheTripResponse = reservation.Trip?.ToResponse(),
                TheUserDTO = reservation.User?.ToUserDTO()
            };
        }
    }
}
