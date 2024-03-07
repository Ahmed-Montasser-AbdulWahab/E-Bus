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

        public TripsService(IGetterRepository<Trip> getterTripRepository)
        {
            this.getterTripRepository = getterTripRepository;
        }

        public async Task<List<TripResponse>?> GetAllTripsAsync()
        {
            return (await getterTripRepository.GetAllAsync())?.Select(trip => trip.ToResponse()).ToList();
        }

        public async Task<List<TripResponse>?> GetUpcomingTripsAsync()
        {
            return (await getterTripRepository.GetAllAsync())?.Where(trip => trip.DepartureTime.CompareTo(DateTime.Now) < 1).Select(trip => trip.ToResponse()).ToList();
        }
    }
}
