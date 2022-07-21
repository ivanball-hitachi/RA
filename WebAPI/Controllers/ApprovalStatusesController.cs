using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/approvalstatuses")]
    public class ApprovalStatusesController : EntityControllerBase<ApprovalStatus, ApprovalStatusDTO, ApprovalStatusForCreationDTO, ApprovalStatusForUpdateDTO, int>
    {
        public ApprovalStatusesController(IEntityService<ApprovalStatus> entityService, ILogger<EntityControllerBase<ApprovalStatus, ApprovalStatusDTO, ApprovalStatusForCreationDTO, ApprovalStatusForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "ApprovalStatus")
        {
        }

        [HttpGet("{id}", Name = "GetApprovalStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<ApprovalStatusDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}