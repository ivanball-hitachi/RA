using Domain.Common;

namespace Application.Common.Interface
{
    public interface IEntityService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(bool includeChildren = false);
        Task<(IEnumerable<T>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false);
        Task<T> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}