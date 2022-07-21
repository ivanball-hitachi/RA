using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/legalentities")]
    public class LegalEntitiesController : EntityControllerBase<LegalEntity, LegalEntityDTO, LegalEntityForCreationDTO, LegalEntityForUpdateDTO, int>
    {
        public LegalEntitiesController(IEntityService<LegalEntity> entityService, ILogger<EntityControllerBase<LegalEntity, LegalEntityDTO, LegalEntityForCreationDTO, LegalEntityForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "LegalEntity")
        {
        }

        [HttpGet("{id}", Name = "GetLegalEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<LegalEntityDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}