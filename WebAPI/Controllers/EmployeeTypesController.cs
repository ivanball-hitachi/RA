using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/employeetypes")]
    public class EmployeeTypesController : EntityControllerBase<EmployeeType, EmployeeTypeDTO, EmployeeTypeForCreationDTO, EmployeeTypeForUpdateDTO, int>
    {
        public EmployeeTypesController(IEntityService<EmployeeType> entityService, ILogger<EntityControllerBase<EmployeeType, EmployeeTypeDTO, EmployeeTypeForCreationDTO, EmployeeTypeForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "EmployeeType")
        {
        }

        [HttpGet("{id}", Name = "GetEmployeeType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<EmployeeTypeDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}