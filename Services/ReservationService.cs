using E_Bus.Entities.Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.ReservationDTO;
using ServiceContracts.DTOs.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReservationService : IReservationService
    {
        private readonly IAdderRepository<Reservation> adderRepository;
        private readonly IReservationGetterRepository getterRepository;
        private readonly IDeleterRepository<Reservation> deleterRepository;

        public ReservationService(IAdderRepository<Reservation> adderRepository, IReservationGetterRepository getterRepository, IDeleterRepository<Reservation> deleterRepository)
        {
            this.adderRepository = adderRepository;
            this.getterRepository = getterRepository;
            this.deleterRepository = deleterRepository;
        }

        public async Task<bool> AddReservationAsync(ReservationAddRequest request)
        {
            if (await getterRepository.GetByIdAsync($"{request.TripId}{request.UserId}") is not null)
            {  return false ; }
            var reservation = request.ToReservation();
            return await adderRepository.AddAsync(reservation);
            
        }

        public async Task<bool> DeleteReservation(ReservationId reservationId)
        {
            var reservation = await getterRepository.GetByIdAsync($"{reservationId.TripId}{reservationId.UserId}");
            if (reservation is null) return false ;
            return await deleterRepository.DeleteAsync(reservation);
        }

        public async Task<ReservationResponse?> GetReservationById(object id, bool include = false)
        {
            var reservation = await getterRepository.GetByIdAsync(id, include);

            if (reservation is null) return null;

            return new ReservationResponse()
            {
                TheTripResponse = reservation?.Trip?.ToResponse() ,
                TheUserDTO = reservation?.User?.ToUserDTO()
            };
        }

        public async Task<List<ReservationResponse>?> GetReservationByUserId(Guid id, bool include = true)
        {
                return (await getterRepository.GetByUserIdAsync(id, include))?.Select(r => r.ToResponse()).ToList();
        }


    }
}
