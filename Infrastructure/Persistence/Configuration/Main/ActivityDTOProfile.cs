using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class ActivityDTOProfile : Profile
    {
        public ActivityDTOProfile()
        {
            CreateMap<Activity, ActivityDTO>().ReverseMap();
            CreateMap<Activity, ActivityForCreationDTO>().ReverseMap();
            CreateMap<Activity, ActivityForUpdateDTO>().ReverseMap();

            CreateMap<ActivityDTO, ActivityForCreationDTO>();
            CreateMap<ActivityDTO, ActivityForUpdateDTO>();
        }
    }
}