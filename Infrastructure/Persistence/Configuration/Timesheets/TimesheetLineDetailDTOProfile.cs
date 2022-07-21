using AutoMapper;
using Domain.Timesheets;
using Domain.Timesheets.DTO;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    public class TimesheetLineDetailDTOProfile : Profile
    {
        public TimesheetLineDetailDTOProfile()
        {
            CreateMap<TimesheetLineDetail, TimesheetLineDetailDTO>().ReverseMap();
            CreateMap<TimesheetLineDetail, TimesheetLineDetailForCreationDTO>().ReverseMap();
            CreateMap<TimesheetLineDetail, TimesheetLineDetailForUpdateDTO>().ReverseMap();

            CreateMap<TimesheetLineDetailDTO, TimesheetLineDetailForCreationDTO>();
            CreateMap<TimesheetLineDetailDTO, TimesheetLineDetailForUpdateDTO>();
        }
    }
}