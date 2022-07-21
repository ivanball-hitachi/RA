using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : EntityControllerBase<Category, CategoryDTO, CategoryForCreationDTO, CategoryForUpdateDTO, int>
    {
        public CategoriesController(IEntityService<Category> entityService, ILogger<EntityControllerBase<Category, CategoryDTO, CategoryForCreationDTO, CategoryForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Category")
        {
        }

        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<CategoryDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}