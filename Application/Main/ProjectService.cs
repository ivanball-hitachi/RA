using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class ProjectService : IEntityService<Project>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ProjectService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<Project>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.ProjectRepository
                .TableNoTracking
                .OrderBy(p => p.Code)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Project>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.ProjectRepository.Table;

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(p =>
                    p.Name.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.Code)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Project> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.ProjectRepository.Get(id, new string[] { });
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _unitOfWork.ProjectRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Project entity)
        {
            try
            {
                await _unitOfWork.ProjectRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Project entity)
        {
            try
            {
                _unitOfWork.ProjectRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Project entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return IsDeleted;
            }
            catch (Exception)
            {
                //handle exception
                return false;
            }
        }
        #endregion
    }
}