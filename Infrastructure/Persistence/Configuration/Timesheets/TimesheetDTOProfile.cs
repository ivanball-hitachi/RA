using AutoMapper;
using Domain.Timesheets;
using Domain.Timesheets.DTO;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    public class TimesheetDTOProfile : Profile
    {
        public TimesheetDTOProfile()
        {
            CreateMap<Timesheet, TimesheetDTO>()
                .ForMember(dest => dest.ApprovalStatusName, opt => opt.MapFrom(src => src.ApprovalStatus!.Name))
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Employee!.FullName))
                .ReverseMap();
            CreateMap<Timesheet, TimesheetForCreationDTO>().ReverseMap();
            CreateMap<Timesheet, TimesheetForUpdateDTO>().ReverseMap();

            CreateMap<TimesheetDTO, TimesheetForCreationDTO>();
            CreateMap<TimesheetDTO, TimesheetForUpdateDTO>();
        }
    }
}