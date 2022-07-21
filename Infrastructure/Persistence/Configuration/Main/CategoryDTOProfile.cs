using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class CategoryDTOProfile : Profile
    {
        public CategoryDTOProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryForCreationDTO>().ReverseMap();
            CreateMap<Category, CategoryForUpdateDTO>().ReverseMap();

            CreateMap<CategoryDTO, CategoryForCreationDTO>();
            CreateMap<CategoryDTO, CategoryForUpdateDTO>();
        }
    }
}