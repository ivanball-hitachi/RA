using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class LegalEntityService : IEntityService<LegalEntity>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public LegalEntityService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<LegalEntity>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.LegalEntityRepository
                .TableNoTracking
                .OrderBy(p => p.Code)
                .ToListAsync();
        }

        public async Task<(IEnumerable<LegalEntity>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.LegalEntityRepository.Table;

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

        public async Task<LegalEntity> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.LegalEntityRepository.Get(id, new string[] { });
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
                return await _unitOfWork.LegalEntityRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(LegalEntity entity)
        {
            try
            {
                await _unitOfWork.LegalEntityRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(LegalEntity entity)
        {
            try
            {
                _unitOfWork.LegalEntityRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(LegalEntity entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.LegalEntityRepository.Delete(entity);
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