using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class LocationService : IEntityService<Location>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public LocationService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<Location>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.LocationRepository
                .TableNoTracking
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Location>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.LocationRepository.Table;

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

        public async Task<Location> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.LocationRepository.Get(id, new string[] { });
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
                return await _unitOfWork.LocationRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Location entity)
        {
            try
            {
                await _unitOfWork.LocationRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Location entity)
        {
            try
            {
                _unitOfWork.LocationRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Location entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.LocationRepository.Delete(entity);
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