using Domain.Common;
using Domain.Timesheets.DTO;
using System.Net.Http;

namespace RazorClassLibrary.Services;

public interface ITimesheetService : IEntityService<TimesheetDTO, TimesheetForCreationDTO, TimesheetForUpdateDTO, int>
{
    Task<DateTime> GetPeriodStartDate(DateTime periodDate);
    Task<DateTime> GetPeriodEndDate(DateTime periodDate);
    Task<string> GetNewTimesheetNumber();
    Task<bool> CreateAsync(TimesheetForCreationDTO entity, string action = default!);
    Task<bool> UpdateAsync(TimesheetForUpdateDTO entity, string action = default!);
}
