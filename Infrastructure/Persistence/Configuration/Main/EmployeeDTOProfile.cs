using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class EmployeeDTOProfile : Profile
    {
        public EmployeeDTOProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.EmployeeTypeName, opt => opt.MapFrom(src => src.EmployeeType!.Name))
                .ReverseMap();
            CreateMap<Employee, EmployeeForCreationDTO>().ReverseMap();
            CreateMap<Employee, EmployeeForUpdateDTO>().ReverseMap();

            CreateMap<EmployeeDTO, EmployeeForCreationDTO>();
            CreateMap<EmployeeDTO, EmployeeForUpdateDTO>();
        }
    }
}