using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class LinePropertyDTOProfile : Profile
    {
        public LinePropertyDTOProfile()
        {
            CreateMap<LineProperty, LinePropertyDTO>().ReverseMap();
            CreateMap<LineProperty, LinePropertyForCreationDTO>().ReverseMap();
            CreateMap<LineProperty, LinePropertyForUpdateDTO>().ReverseMap();

            CreateMap<LinePropertyDTO, LinePropertyForCreationDTO>();
            CreateMap<LinePropertyDTO, LinePropertyForUpdateDTO>();
        }
    }
}