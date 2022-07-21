using Domain.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public interface IEntityControllerBase<TEntity, TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>
        where TEntity : IBaseEntity<TIdentifier>, IAuditableEntity
        where TEntityDTO : AuditableDTO, IBaseEntity<TIdentifier>
        where TEntityForCreationDTO : AuditableDTO
        where TEntityForUpdateDTO : AuditableDTO, IBaseEntity<TIdentifier>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll(bool includeChildren = false);
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll(string? searchValue, int pageNumber = 1, int pageSize = 10, bool includeChildren = false);
        Task<ActionResult<TEntityDTO>> GetById(TIdentifier id, bool includeChildren = false);
        Task<ActionResult<TEntityDTO>> Create(TEntityForCreationDTO entityDTO, string action = default!);
        Task<ActionResult> Update(TIdentifier id, TEntityForUpdateDTO entityDTO, string action = default!);
        Task<ActionResult> PartiallyUpdate(TIdentifier id, JsonPatchDocument<TEntityForUpdateDTO> patchDocument);
        Task<ActionResult> Delete(TIdentifier id);
    }
}