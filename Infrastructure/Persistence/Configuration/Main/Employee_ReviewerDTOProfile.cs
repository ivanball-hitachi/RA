using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class Employee_ReviewerDTOProfile : Profile
    {
        public Employee_ReviewerDTOProfile()
        {
            CreateMap<Employee_Reviewer, Employee_ReviewerDTO>()
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Employee!.FullName))
                .ForMember(dest => dest.ReviewerFullName, opt => opt.MapFrom(src => src.Reviewer!.FullName))
                .ReverseMap();
            CreateMap<Employee_Reviewer, Employee_ReviewerForCreationDTO>().ReverseMap();
            CreateMap<Employee_Reviewer, Employee_ReviewerForUpdateDTO>().ReverseMap();

            CreateMap<Employee_ReviewerDTO, Employee_ReviewerForCreationDTO>();
            CreateMap<Employee_ReviewerDTO, Employee_ReviewerForUpdateDTO>();
        }
    }
}