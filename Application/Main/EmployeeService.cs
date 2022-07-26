using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class EmployeeService : IEntityService<Employee>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public EmployeeService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<Employee>> GetAllAsync(bool includeChildren = false)
        {
            return await _unitOfWork.EmployeeRepository
                .TableNoTracking
                .OrderBy(p => p.FirstName).ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Employee>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.EmployeeRepository.Table;

            if (includeFKs)
            {
                collection = collection.Include(p => p.EmployeeType);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(p => 
                    p.FirstName.ToUpper().Contains(searchValue.ToUpper()) ||
                    p.LastName.ToUpper().Contains(searchValue.ToUpper()) ||
                    (p.FirstName + ' ' + p.LastName).ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.FirstName).ThenBy(p => p.LastName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Employee> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                var includes = new List<string>();
                if (includeFKs)
                {
                    includes.Add("EmployeeType");
                }

                return await _unitOfWork.EmployeeRepository.Get(id, includes.ToArray());
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
                return await _unitOfWork.EmployeeRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Employee entity)
        {
            try
            {
                await _unitOfWork.EmployeeRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Employee entity)
        {
            try
            {
                _unitOfWork.EmployeeRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Employee entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.EmployeeRepository.Delete(entity);
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