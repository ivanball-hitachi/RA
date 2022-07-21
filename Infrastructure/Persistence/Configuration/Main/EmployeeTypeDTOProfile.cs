using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class EmployeeTypeDTOProfile : Profile
    {
        public EmployeeTypeDTOProfile()
        {
            CreateMap<EmployeeType, EmployeeTypeDTO>().ReverseMap();
            CreateMap<EmployeeType, EmployeeTypeForCreationDTO>().ReverseMap();
            CreateMap<EmployeeType, EmployeeTypeForUpdateDTO>().ReverseMap();

            CreateMap<EmployeeTypeDTO, EmployeeTypeForCreationDTO>();
            CreateMap<EmployeeTypeDTO, EmployeeTypeForUpdateDTO>();
        }
    }
}