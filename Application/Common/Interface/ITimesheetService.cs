using Domain.Common;
using Domain.Timesheets;

namespace Application.Common.Interface
{
    public interface ITimesheetService : IEntityService<Timesheet>
    {
        string GetNewTimesheetNumber();

        Task<IEnumerable<TimesheetLine>> GetTimesheetLinesAsync(int timesheetId, bool includeChildren = false);
        Task<(IEnumerable<TimesheetLine>, PaginationMetadata)> GetTimesheetLinesAsync(int timesheetId,
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false);
        Task<TimesheetLine> GetTimesheetLineByIdAsync(int timesheetId, int id, bool includeFKs = false, bool includeChildren = false);
        Task<TimesheetLine> GetTimesheetLineByIdAsync(int id, bool includeFKs = false, bool includeChildren = false);
        Task<bool> TimesheetLineExistsAsync(int timesheetId, int id);
        Task<bool> TimesheetLineExistsAsync(int id);
        Task AddTimesheetLineAsync(int timesheetId, TimesheetLine entity);
        Task UpdateTimesheetLineAsync(int timesheetId, TimesheetLine entity);
        Task<bool> DeleteTimesheetLineAsync(TimesheetLine entity);

        Task<IEnumerable<TimesheetLineDetail>> GetTimesheetLineDetailsAsync(int timesheetLineId, bool includeChildren = false);
        Task<(IEnumerable<TimesheetLineDetail>, PaginationMetadata)> GetTimesheetLineDetailsAsync(int timesheetLineId,
            string? searchValue, int pageNumber, int pageSize, bool includeFKs = false, bool includeChildren = false);
        Task<TimesheetLineDetail> GetTimesheetLineDetailByIdAsync(int timesheetLineId, int id, bool includeFKs = false, bool includeChildren = false);
        Task<bool> TimesheetLineDetailExistsAsync(int timesheetLineId, int id);
        Task AddTimesheetLineDetailAsync(int timesheetLineId, TimesheetLineDetail entity);
        Task UpdateTimesheetLineDetailAsync(int timesheetLineId, TimesheetLineDetail entity);
        Task<bool> DeleteTimesheetLineDetailAsync(TimesheetLineDetail entity);
    }
}