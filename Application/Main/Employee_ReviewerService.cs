using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Microsoft.EntityFrameworkCore;

namespace Application.Main
{
    internal class Employee_ReviewerService : IEntityService<Employee_Reviewer>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public Employee_ReviewerService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<Employee_Reviewer>> GetAllAsync(bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.Employee_ReviewerRepository.TableNoTracking;

            return await collection
                .OrderBy(p => p.EmployeeId)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Employee_Reviewer>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.Employee_ReviewerRepository.Table;

            if (includeFKs)
            {
                collection = collection.Include(p => p.Employee).Include(p => p.Reviewer);
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.EmployeeId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Employee_Reviewer> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                var includes = new List<string>();
                if (includeFKs)
                {
                    includes.Add("Employee");
                    includes.Add("Reviewer");
                }

                return await _unitOfWork.Employee_ReviewerRepository.Get(id, includes.ToArray());
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
                return await _unitOfWork.Employee_ReviewerRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Employee_Reviewer entity)
        {
            try
            {
                await _unitOfWork.Employee_ReviewerRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Employee_Reviewer entity)
        {
            try
            {
                _unitOfWork.Employee_ReviewerRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Employee_Reviewer entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.Employee_ReviewerRepository.Delete(entity);
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