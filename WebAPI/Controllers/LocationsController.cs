using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : EntityControllerBase<Location, LocationDTO, LocationForCreationDTO, LocationForUpdateDTO, int>
    {
        public LocationsController(IEntityService<Location> entityService, ILogger<EntityControllerBase<Location, LocationDTO, LocationForCreationDTO, LocationForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Location")
        {
        }

        [HttpGet("{id}", Name = "GetLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<LocationDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}