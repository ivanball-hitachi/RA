using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.Json;
using Application.Common.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntityControllerBase<TEntity, TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier> : ControllerBase, IEntityControllerBase<TEntity, TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>
        where TEntity : IBaseEntity<TIdentifier>, IAuditableEntity
        where TEntityDTO : AuditableDTO, IBaseEntity<TIdentifier>
        where TEntityForCreationDTO : AuditableDTO
        where TEntityForUpdateDTO : AuditableDTO, IBaseEntity<TIdentifier>
    {
        private readonly string _entityName;
        protected readonly IEntityService<TEntity> _entityService;
        private readonly ILogger<EntityControllerBase<TEntity, TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>> _logger;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;

        public EntityControllerBase(IEntityService<TEntity> entityService,
            ILogger<EntityControllerBase<TEntity, TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>> logger, IMapper mapper, string entityName)
        {
            _entityName = entityName;
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll(bool includeChildren = false)
        {
            var entities = await _entityService.GetAllAsync(includeChildren);

            return Ok(_mapper.Map<IEnumerable<TEntityDTO>>(entities));
        }

        [HttpGet("paged")]
        public virtual async Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll(
            string? searchValue, int pageNumber = 1, int pageSize = 10, bool includeChildren = false)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (entities, paginationMetadata) = await _entityService
                .GetAllAsync(searchValue, pageNumber, pageSize, true, includeChildren);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<TEntityDTO>>(entities));
        }

        [HttpGet("{id}")] //, Name = "Get{_entityName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult<TEntityDTO>> GetById(TIdentifier id, bool includeChildren = false)
        {
            var entity = await _entityService.GetByIdAsync(Convert.ToInt32(id), true, includeChildren);
            if (entity is null)
            {
                _logger.LogInformation(
                    $"{_entityName} with Id {id} wasn't found.");
                return NotFound();
            }

            return Ok(_mapper.Map<TEntityDTO>(entity));
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntityDTO>> Create(TEntityForCreationDTO entityDTO, string action = default!)
        {
            var entity = _mapper.Map<TEntity>(entityDTO);

            await _entityService.AddAsync(entity);

            var createdEntityToReturn =
                _mapper.Map<TEntityDTO>(entity);

            return CreatedAtRoute($"Get{_entityName}",
                 new
                 {
                     id = createdEntityToReturn.Id
                 },
                 createdEntityToReturn);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update(TIdentifier id, TEntityForUpdateDTO entityDTO, string action = default!)
        {
            var entity = await _entityService.GetByIdAsync(Convert.ToInt32(id));
            if (entity is null)
            {
                return NotFound();
            }

            await _entityService.UpdateAsync(_mapper.Map(entityDTO, entity));

            return NoContent();
        }


        [HttpPatch("{id}")]
        public virtual async Task<ActionResult> PartiallyUpdate(TIdentifier id,
            JsonPatchDocument<TEntityForUpdateDTO> patchDocument)
        {
            var entity = await _entityService.GetByIdAsync(Convert.ToInt32(id));
            if (entity is null)
            {
                return NotFound();
            }

            var entityToPatch = _mapper.Map<TEntityForUpdateDTO>(entity);

            patchDocument.ApplyTo(entityToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(entityToPatch))
            {
                return BadRequest(ModelState);
            }

            await _entityService.UpdateAsync(_mapper.Map(entityToPatch, entity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(TIdentifier id)
        {
            var entity = await _entityService.GetByIdAsync(Convert.ToInt32(id));
            if (entity is null)
            {
                return NotFound();
            }

            var isDeleted = await _entityService.DeleteAsync(entity);

            return NoContent();
        }
    }
}