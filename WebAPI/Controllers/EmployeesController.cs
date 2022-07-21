using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : EntityControllerBase<Employee, EmployeeDTO, EmployeeForCreationDTO, EmployeeForUpdateDTO, int>
    {
        public EmployeesController(IEntityService<Employee> entityService, ILogger<EntityControllerBase<Employee, EmployeeDTO, EmployeeForCreationDTO, EmployeeForUpdateDTO, int>> logger, IMapper mapper) 
            : base(entityService, logger, mapper, "Employee")
        {
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<EmployeeDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}