using Application.Common.Interface;
using Domain.Common;
using Domain.Timesheets;
using Microsoft.EntityFrameworkCore;

namespace Application.Timesheets
{
    internal class TimesheetService : ITimesheetService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public TimesheetService(IUnitOfWork unitOfWork)
{
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        public string GetNewTimesheetNumber()
        {
            try
            {
                return _unitOfWork.TimesheetRepository.GetNewTimesheetNumber();
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        #region Queries
        public async Task<IEnumerable<Timesheet>> GetAllAsync(bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetRepository.TableNoTracking;

            if (includeChildren)
            {
                collection = collection.Include(ts => ts.TimesheetLines).ThenInclude(tsl => tsl.TimesheetLineDetails);
            }

            return await collection
                .OrderBy(ts => ts.TimesheetNumber)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Timesheet>, PaginationMetadata)> GetAllAsync(
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetRepository.Table;

            if (includeFKs)
            {
                collection = collection.Include(ts => ts.Employee).Include(ts => ts.ApprovalStatus);
            }

            if (includeChildren)
            {
                collection = collection.Include(ts => ts.TimesheetLines).ThenInclude(tsl => tsl.TimesheetLineDetails);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(ts =>
                    ts.TimesheetNumber.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(ts => ts.TimesheetNumber)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Timesheet> GetByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                var includes = new List<string>();
                if (includeFKs)
                {
                    includes.Add("Employee");
                    includes.Add("ApprovalStatus");
                }
                if (includeChildren)
                {
                    includes.Add("TimesheetLines");
                }

                return await _unitOfWork.TimesheetRepository.Get(id, includes.ToArray());
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
                return await _unitOfWork.TimesheetRepository.Exists(id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region Command
        public async Task AddAsync(Timesheet entity)
        {
            try
            {
                await _unitOfWork.TimesheetRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateAsync(Timesheet entity)
        {
            try
            {
                _unitOfWork.TimesheetRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Timesheet entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.TimesheetRepository.Delete(entity);
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

        #region TimesheetLine Queries
        public async Task<IEnumerable<TimesheetLine>> GetTimesheetLinesAsync(int timesheetId, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetLineRepository.TableNoTracking;

            if (includeChildren)
            {
                collection = collection.Include(tsl => tsl.TimesheetLineDetails);
            }

            return await collection
                .Where(tsl => tsl.TimesheetId == timesheetId)
                .OrderBy(tsl => tsl.ProjectId)
                .ToListAsync();
        }

        public async Task<(IEnumerable<TimesheetLine>, PaginationMetadata)> GetTimesheetLinesAsync(int timesheetId,
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetLineRepository.Table.Where(tsl => tsl.TimesheetId == timesheetId);

            if (includeFKs)
            {
                collection = collection.Include(tsl => tsl.LegalEntity)
                                        .Include(tsl => tsl.Location)
                                        .Include(tsl => tsl.Customer)
                                        .Include(tsl => tsl.Project)
                                        .Include(tsl => tsl.Activity)
                                        .Include(tsl => tsl.Category)
                                        .Include(tsl => tsl.LineProperty)
                                        .Include(tsl => tsl.ApprovalStatus)
                                        .Include(tsl => tsl.Timesheet)
                                        .ThenInclude(ts => ts.Employee)
                                        .ThenInclude(e => e!.EmployeeType);
            }

            if (includeChildren)
            {
                collection = collection.Include(tsl => tsl.TimesheetLineDetails);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(tsl =>
                    tsl.Project!.Name.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(tsl => tsl.ProjectId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<TimesheetLine> GetTimesheetLineByIdAsync(int timesheetId, int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                var includes = new List<string>();
                if (includeFKs)
                {
                    includes.Add("LegalEntity");
                    includes.Add("Location");
                    includes.Add("Customer");
                    includes.Add("Project");
                    includes.Add("Activity");
                    includes.Add("Category");
                    includes.Add("LineProperty");
                    includes.Add("ApprovalStatus");
                }
                if (includeChildren)
                {
                    includes.Add("TimesheetLineDetails");
                }

                return await _unitOfWork.TimesheetLineRepository
                    .Get(tsl => tsl.TimesheetId == timesheetId && tsl.Id == id, includes.ToArray());
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<TimesheetLine> GetTimesheetLineByIdAsync(int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                var includes = new List<string>();
                if (includeFKs)
                {
                    includes.Add("LegalEntity");
                    includes.Add("Location");
                    includes.Add("Customer");
                    includes.Add("Project");
                    includes.Add("Activity");
                    includes.Add("Category");
                    includes.Add("LineProperty");
                }
                if (includeChildren)
                {
                    includes.Add("TimesheetLineDetails");
                }

                return await _unitOfWork.TimesheetLineRepository
                    .Get(tsl => tsl.Id == id, includes.ToArray());
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<bool> TimesheetLineExistsAsync(int timesheetId, int id)
        {
            try
            {
                return await _unitOfWork.TimesheetLineRepository.Exists(tsl => tsl.TimesheetId == timesheetId && tsl.Id == id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<bool> TimesheetLineExistsAsync(int id)
        {
            try
            {
                return await _unitOfWork.TimesheetLineRepository.Exists(tsl => tsl.Id == id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region TimesheetLine Command
        public async Task AddTimesheetLineAsync(int timesheetId, TimesheetLine entity)
        {
            try
            {
                var timesheet = await GetByIdAsync(timesheetId, true);
                if (timesheet is not null)
                {
                    await _unitOfWork.TimesheetLineRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateTimesheetLineAsync(int timesheetId, TimesheetLine entity)
        {
            try
            {
                var timesheet = await GetByIdAsync(timesheetId, true);
                if (timesheet is not null)
                {
                    _unitOfWork.TimesheetLineRepository.Update(entity);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteTimesheetLineAsync(TimesheetLine entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.TimesheetLineRepository.Delete(entity);
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

        #region TimesheetLineDetail Queries
        public async Task<IEnumerable<TimesheetLineDetail>> GetTimesheetLineDetailsAsync(int timesheetLineId, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetLineDetailRepository.TableNoTracking;

            return await collection
                .Where(tsld => tsld.TimesheetLineId == timesheetLineId)
                .OrderBy(tsld => tsld.Day)
                .ToListAsync();
        }

        public async Task<(IEnumerable<TimesheetLineDetail>, PaginationMetadata)> GetTimesheetLineDetailsAsync(int timesheetLineId,
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false)
        {
            // collection to start from
            var collection = _unitOfWork.TimesheetLineDetailRepository.Table.Where(tsld => tsld.TimesheetLineId == timesheetLineId);

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                collection = collection.Where(tsld =>
                    tsld.InternalComment!.ToUpper().Contains(searchValue.ToUpper()) ||
                    tsld.ExternalComment!.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(tsld => tsld.Day)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<TimesheetLineDetail> GetTimesheetLineDetailByIdAsync(int timesheetLineId, int id, bool includeFKs = false, bool includeChildren = false)
        {
            try
            {
                return await _unitOfWork.TimesheetLineDetailRepository
                    .Get(tsld => tsld.TimesheetLineId == timesheetLineId && tsld.Id == id, new string[] { });
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<bool> TimesheetLineDetailExistsAsync(int timesheetLineId, int id)
        {
            try
            {
                return await _unitOfWork.TimesheetLineDetailRepository.Exists(tsld => tsld.TimesheetLineId == timesheetLineId && tsld.Id == id);
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

        #region TimesheetLineDetail Command
        public async Task AddTimesheetLineDetailAsync(int timesheetLineId, TimesheetLineDetail entity)
        {
            try
            {
                var timesheetLine = await GetTimesheetLineByIdAsync(timesheetLineId, true);
                if (timesheetLine is not null)
                {
                    await _unitOfWork.TimesheetLineDetailRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task UpdateTimesheetLineDetailAsync(int timesheetLineId, TimesheetLineDetail entity)
        {
            try
            {
                var timesheetLine = await GetTimesheetLineByIdAsync(timesheetLineId, true);
                if (timesheetLine is not null)
                {
                    _unitOfWork.TimesheetLineDetailRepository.Update(entity);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteTimesheetLineDetailAsync(TimesheetLineDetail entity)
        {
            try
            {
                var IsDeleted = _unitOfWork.TimesheetLineDetailRepository.Delete(entity);
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