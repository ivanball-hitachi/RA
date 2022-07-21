using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class CustomerService : IEntityService<Customer>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CustomerService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<Customer>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.CustomerRepository
                .TableNoTracking
                .OrderBy(p => p.CustomerAccount)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Customer>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.CustomerRepository.Table;

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(p =>
                    p.Name.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.CustomerAccount)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Customer> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.CustomerRepository.Get(id, new string[] { });
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
                return await _unitOfWork.CustomerRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Customer entity)
        {
            try
            {
                await _unitOfWork.CustomerRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Customer entity)
        {
            try
            {
                _unitOfWork.CustomerRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Customer entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.CustomerRepository.Delete(entity);
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