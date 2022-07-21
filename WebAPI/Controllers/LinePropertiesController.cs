using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/lineproperties")]
    public class LinePropertiesController : EntityControllerBase<LineProperty, LinePropertyDTO, LinePropertyForCreationDTO, LinePropertyForUpdateDTO, int>
    {
        public LinePropertiesController(IEntityService<LineProperty> entityService, ILogger<EntityControllerBase<LineProperty, LinePropertyDTO, LinePropertyForCreationDTO, LinePropertyForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "LineProperty")
        {
        }

        [HttpGet("{id}", Name = "GetLineProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<LinePropertyDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}