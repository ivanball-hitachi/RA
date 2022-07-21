using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class LinePropertyService : IEntityService<LineProperty>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public LinePropertyService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<LineProperty>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.LinePropertyRepository
                .TableNoTracking
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<(IEnumerable<LineProperty>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.LinePropertyRepository.Table;

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(p =>
                    p.Name.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<LineProperty> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.LinePropertyRepository.Get(id, new string[] { });
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
                return await _unitOfWork.LinePropertyRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(LineProperty entity)
        {
            try
            {
                await _unitOfWork.LinePropertyRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(LineProperty entity)
        {
            try
            {
                _unitOfWork.LinePropertyRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(LineProperty entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.LinePropertyRepository.Delete(entity);
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