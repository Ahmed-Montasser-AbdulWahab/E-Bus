using ServiceContracts.DTOs;
using ServiceContracts.DTOs.ReservationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IReservationService
    {
        public Task<bool> AddReservationAsync(ReservationAddRequest request);
        public Task<UserTripWrapperModel?> GetReservationById(object id, bool include = false);
    }
}
