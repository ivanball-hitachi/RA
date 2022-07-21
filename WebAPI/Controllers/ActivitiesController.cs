using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : EntityControllerBase<Activity, ActivityDTO, ActivityForCreationDTO, ActivityForUpdateDTO, int>
    {
        public ActivitiesController(IEntityService<Activity> entityService, ILogger<EntityControllerBase<Activity, ActivityDTO, ActivityForCreationDTO, ActivityForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Activity")
        {
        }

        [HttpGet("{id}", Name = "GetActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<ActivityDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}