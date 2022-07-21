using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class LegalEntityDTOProfile : Profile
    {
        public LegalEntityDTOProfile()
        {
            CreateMap<LegalEntity, LegalEntityDTO>().ReverseMap();
            CreateMap<LegalEntity, LegalEntityForCreationDTO>().ReverseMap();
            CreateMap<LegalEntity, LegalEntityForUpdateDTO>().ReverseMap();

            CreateMap<LegalEntityDTO, LegalEntityForCreationDTO>();
            CreateMap<LegalEntityDTO, LegalEntityForUpdateDTO>();
        }
    }
}