using Domain.Common;
using Domain.Timesheets.DTO;

namespace RazorClassLibrary.Services;

public interface ITimesheetLineService
{
    Task<(List<TimesheetLineDTO>?, PaginationMetadata?)> GetTimesheetLinesAsync(int timesheetId, string? searchValue = default, int pageNumber = 1, int pageSize = 10);
    Task<TimesheetLineDTO?> GetTimesheetLineByIdAsync(int timesheetId, int timesheetLineId, string? searchValueForTimesheetLineDetails = default);
    Task<bool> AddTimesheetLineAsync(int timesheetId, TimesheetLineForCreationDTO timesheetLine);
    Task<bool> UpdateTimesheetLineAsync(int timesheetId, TimesheetLineForUpdateDTO timesheetLine);
    Task<bool> DeleteTimesheetLineAsync(int timesheetId, int timesheetLineId);
}
