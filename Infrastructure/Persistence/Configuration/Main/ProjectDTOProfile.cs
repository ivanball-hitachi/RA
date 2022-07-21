using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class ProjectDTOProfile : Profile
    {
        public ProjectDTOProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Project, ProjectForCreationDTO>().ReverseMap();
            CreateMap<Project, ProjectForUpdateDTO>().ReverseMap();

            CreateMap<ProjectDTO, ProjectForCreationDTO>();
            CreateMap<ProjectDTO, ProjectForUpdateDTO>();
        }
    }
}