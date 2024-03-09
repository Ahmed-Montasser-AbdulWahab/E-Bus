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
        private readonly IGetterRepository<Reservation> getterRepository;



        public ReservationService(IAdderRepository<Reservation> adderRepository, IGetterRepository<Reservation> getterRepository)
        {
            this.adderRepository = adderRepository;
            this.getterRepository = getterRepository;
        }

        public async Task<bool> AddReservationAsync(ReservationAddRequest request)
        {
            if (await getterRepository.GetByIdAsync($"{request.TripId}{request.UserId}") is not null)
            {  return false ; }
            var reservation = request.ToReservation();
            return await adderRepository.AddAsync(reservation);
            
        }

        public async Task<UserTripWrapperModel?> GetReservationById(object id, bool include = false)
        {
            var reservation = await getterRepository.GetByIdAsync(id, include);

            if (reservation is null) return null;

            return new UserTripWrapperModel()
            {
                TheTripResponse = reservation?.Trip?.ToResponse() ,
                TheUserDTO = reservation?.User?.ToUserDTO()
            };
        }
    }
}
