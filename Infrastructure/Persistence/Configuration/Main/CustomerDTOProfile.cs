using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class CustomerDTOProfile : Profile
    {
        public CustomerDTOProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerForCreationDTO>().ReverseMap();
            CreateMap<Customer, CustomerForUpdateDTO>().ReverseMap();

            CreateMap<CustomerDTO, CustomerForCreationDTO>();
            CreateMap<CustomerDTO, CustomerForUpdateDTO>();
        }
    }
}