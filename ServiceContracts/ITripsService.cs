using ServiceContracts.DTOs.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITripsService
    {
        public Task<List<TripResponse>?> GetAllTripsAsync();
        public Task<List<TripResponse>?> GetUpcomingTripsAsync();

        public Task<bool> AddTripAsync(TripAddRequest tripAddRequest);

        public Task<TripResponse?> GetTripByIdAsync(Guid tripId, bool include = false);
        public Task<TripUpdateRequest?> GetTripByIdAsync(Guid tripId);

        public Task<bool> DeleteTripAsync(Guid tripId);

        public Task<bool> UpdateTripAsync(TripUpdateRequest tripUpdateRequest);
    }
}
