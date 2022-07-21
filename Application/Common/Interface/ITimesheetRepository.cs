using Domain.Common;
using Domain.Timesheets;
using System.Linq.Expressions;

namespace Application.Common.Interface
{
    public interface ITimesheetRepository : IRepository<Timesheet, int>
    {
        string GetNewTimesheetNumber();
    }
}