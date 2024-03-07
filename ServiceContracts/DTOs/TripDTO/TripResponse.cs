using E_Bus.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.TripDTO
{
    public class TripResponse
    {

        public string? Id { get; set; }

        public string? Starting { get; set; }

        public string? Ending { get; set; }

        public string? ServiceTypeName { get; set; }

        public string? Ticket { get; set; }

        public string? DepartureTime { get; set; }

        public string? ArrivalTime { get; set; }

        public string? RemainingSeats { get; set; }

        public string? NumberOfSeats { get; set; }
    }

    public static class TripsExtensions
    {

        public static TripResponse ToResponse(this Trip trip)
        {
            return new TripResponse()
            {
                ArrivalTime = trip.ArrivalTime.ToString("dddd, dd MMMM yyyy"),
                DepartureTime = trip.DepartureTime.ToString("dddd, dd MMMM yyyy"),
                Ending = trip.Ending,
                Starting = trip.Starting,
                Id = trip.Id.ToString(),
                ServiceTypeName = trip.ServiceType!.Name,
                Ticket = trip.Ticket.ToString("N"),
                NumberOfSeats = trip.ServiceType.Seats.ToString(),
                RemainingSeats = $"{trip.ServiceType.Seats - (trip.Reservations?.Count ?? 0)}"
            };
        }

    }
}
