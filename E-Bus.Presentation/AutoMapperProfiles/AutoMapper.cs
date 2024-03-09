using AutoMapper;
using E_Bus.Entities.Entities;

namespace E_Bus.Presentation.AutoMapperProfiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {

            CreateMap<Trip, Trip>();
            CreateMap<Reservation, Reservation>();
        }

    }
}
