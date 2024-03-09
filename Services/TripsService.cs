using E_Bus.Entities.Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TripsService : ITripsService
    {
        private readonly IGetterRepository<Trip> getterTripRepository;
        private readonly IAdderRepository<Trip> adderRepository;
        private readonly IDeleterRepository<Trip> deleterRepository;
        private readonly IUpdaterRepository<Trip> updaterRepository;

        public TripsService(IGetterRepository<Trip> getterTripRepository, IAdderRepository<Trip> adderRepository, IDeleterRepository<Trip> deleterRepository, IUpdaterRepository<Trip> updaterRepository)
        {
            this.getterTripRepository = getterTripRepository;
            this.adderRepository = adderRepository;
            this.deleterRepository = deleterRepository;
            this.updaterRepository = updaterRepository;
        }

        public async Task<List<TripResponse>?> GetAllTripsAsync()
        {
            return (await getterTripRepository.GetAllAsync(true))?.Select(trip => trip.ToResponse()).ToList();
        }

        public async Task<List<TripResponse>?> GetUpcomingTripsAsync()
        {
            return (await getterTripRepository.GetAllAsync(true))?.Where(trip => trip.DepartureTime.CompareTo(DateTime.Now) > 0).Select(trip => trip.ToResponse()).ToList();
        }

        public async Task<bool> AddTripAsync(TripAddRequest tripAddRequest)
        {
            var trip = tripAddRequest.ToTrip();
            return await adderRepository.AddAsync(trip);
            
        }

        public async Task<TripResponse?> GetTripByIdAsync(Guid tripId, bool include = false)
        {
            var trip = await getterTripRepository.GetByIdAsync(tripId, include);
            if(trip is null) return null;

            return trip.ToResponse();
        }

        public async Task<TripUpdateRequest?> GetTripByIdAsync(Guid tripId)
        {
            var trip = await getterTripRepository.GetByIdAsync(tripId);
            if (trip is null) return null;

            return trip.ToTripUpdateRequest();
        }

        public async Task<bool> DeleteTripAsync(Guid tripId)
        {
            var tripFromDb = await getterTripRepository.GetByIdAsync(tripId);
            if(tripFromDb is null) return false;

            return (await deleterRepository.DeleteAsync(tripFromDb));

        }

        public async Task<bool> UpdateTripAsync(TripUpdateRequest tripUpdateRequest)
        {
            var trip = tripUpdateRequest.ToTrip();
            return await updaterRepository.UpdateAsync(trip);
        }
    }
}
