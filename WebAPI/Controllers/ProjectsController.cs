using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : EntityControllerBase<Project, ProjectDTO, ProjectForCreationDTO, ProjectForUpdateDTO, int>
    {
        public ProjectsController(IEntityService<Project> entityService, ILogger<EntityControllerBase<Project, ProjectDTO, ProjectForCreationDTO, ProjectForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Project")
        {
        }

        [HttpGet("{id}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<ProjectDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}