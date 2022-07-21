using Domain.Common;

namespace RazorClassLibrary.Services
{
    public interface IEntityService<TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier> : IDisposable
        where TEntityDTO : AuditableDTO, IBaseEntity<TIdentifier>
        where TEntityForCreationDTO : AuditableDTO
        where TEntityForUpdateDTO : AuditableDTO, IBaseEntity<TIdentifier>
    {
        Task<IEnumerable<TEntityDTO>?> GetAllAsync();
        Task<(IEnumerable<TEntityDTO>?, PaginationMetadata?)> GetAllAsync(string? searchValue, int pageNumber = 1, int pageSize = 10);
        Task<TEntityDTO?> GetByIdAsync(TIdentifier identifier);
        Task<bool> CreateAsync(TEntityForCreationDTO entity);
        Task<bool> UpdateAsync(TEntityForUpdateDTO entity);
        Task<bool> DeleteAsync(TIdentifier identifier);
    }
}