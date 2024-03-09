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

        public TripsService(IGetterRepository<Trip> getterTripRepository, IAdderRepository<Trip> adderRepository)
        {
            this.getterTripRepository = getterTripRepository;
            this.adderRepository = adderRepository;
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
    }
}
