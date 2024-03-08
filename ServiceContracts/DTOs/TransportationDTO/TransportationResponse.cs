using E_Bus.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.TransportationDTO
{
    public class TransportationResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Seats { get; set; }

        public string? IsAirConditioned { get; set; }
    }

    public static class TransportationExtensions
    {
        public static TransportationResponse ToResponse(this Transportation transportation)
        {
            return new TransportationResponse
            {
                Id = transportation.Id,
                Name = transportation.Name,
                Seats = transportation.Seats,
                IsAirConditioned = (transportation.IsAirConditioned)? "Air Conditioned" : "Not Air Conditioned"
            };
        }
    }
}
