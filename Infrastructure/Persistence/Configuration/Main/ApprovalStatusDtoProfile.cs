using AutoMapper;
using Domain.Main;
using Domain.Main.DTO;

namespace Infrastructure.Persistence.Configuration.Main
{
    public class ApprovalStatusDTOProfile : Profile
    {
        public ApprovalStatusDTOProfile()
        {
            CreateMap<ApprovalStatus, ApprovalStatusDTO>().ReverseMap();
            CreateMap<ApprovalStatus, ApprovalStatusForCreationDTO>().ReverseMap();
            CreateMap<ApprovalStatus, ApprovalStatusForUpdateDTO>().ReverseMap();

            CreateMap<ApprovalStatusDTO, ApprovalStatusForCreationDTO>();
            CreateMap<ApprovalStatusDTO, ApprovalStatusForUpdateDTO>();
        }
    }
}