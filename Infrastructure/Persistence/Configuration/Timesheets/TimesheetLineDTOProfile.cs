using AutoMapper;
using Domain.Timesheets;
using Domain.Timesheets.DTO;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    public class TimesheetLineDTOProfile : Profile
    {
        public TimesheetLineDTOProfile()
        {
            CreateMap<TimesheetLine, TimesheetLineDTO>()
                .ForMember(dest => dest.LegalEntityName, opt => opt.MapFrom(src => src.LegalEntity!.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location!.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.Name))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project!.Name))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Activity!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.LinePropertyName, opt => opt.MapFrom(src => src.LineProperty!.Name))
                .ForMember(dest => dest.ApprovalStatusName, opt => opt.MapFrom(src => src.ApprovalStatus!.Name))
                .ForMember(dest => dest.EmployeeTypeName, opt => opt.MapFrom(src => src.Timesheet!.Employee!.EmployeeType!.Name))
                .ForMember(dest => dest.PeriodStartDate, opt => opt.MapFrom(src => src.Timesheet.PeriodStartDate))
                .ForMember(dest => dest.PeriodEndDate, opt => opt.MapFrom(src => src.Timesheet.PeriodEndDate))
                .ForPath(dest => dest.TimesheetLineDetails, opt => opt.MapFrom(src => src.TimesheetLineDetails))
                .ReverseMap();
            CreateMap<TimesheetLine, TimesheetLineForCreationDTO>().ReverseMap();
            CreateMap<TimesheetLine, TimesheetLineForUpdateDTO>().ReverseMap();

            CreateMap<TimesheetLineDTO, TimesheetLineForCreationDTO>()
                .ForPath(dest => dest.TimesheetLineDetails, opt => opt.MapFrom(src => src.TimesheetLineDetails));
            CreateMap<TimesheetLineDTO, TimesheetLineForUpdateDTO>()
                .ForPath(dest => dest.TimesheetLineDetails, opt => opt.MapFrom(src => src.TimesheetLineDetails));
        }
    }
}