using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/employee_reviewers")]
    public class Employee_ReviewersController : EntityControllerBase<Employee_Reviewer, Employee_ReviewerDTO, Employee_ReviewerForCreationDTO, Employee_ReviewerForUpdateDTO, int>
    {
        public Employee_ReviewersController(IEntityService<Employee_Reviewer> entityService, ILogger<EntityControllerBase<Employee_Reviewer, Employee_ReviewerDTO, Employee_ReviewerForCreationDTO, Employee_ReviewerForUpdateDTO, int>> logger, IMapper mapper) 
            : base(entityService, logger, mapper, "Employee_Reviewer")
        {
        }

        [HttpGet("{id}", Name = "GetEmployee_Reviewer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<Employee_ReviewerDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}