using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class LocationDTOProfile : Profile
    {
        public LocationDTOProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, LocationForCreationDTO>().ReverseMap();
            CreateMap<Location, LocationForUpdateDTO>().ReverseMap();

            CreateMap<LocationDTO, LocationForCreationDTO>();
            CreateMap<LocationDTO, LocationForUpdateDTO>();
        }
    }
}