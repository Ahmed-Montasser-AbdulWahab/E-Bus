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
        public Task<ReservationResponse?> GetReservationById(object id, bool include = true);
        public Task<List<ReservationResponse>?> GetReservationByUserId(Guid id, bool include = true);

        public Task<bool> DeleteReservation(ReservationId reservationId);
    }
}
