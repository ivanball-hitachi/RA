using Application.Common.Interface;
using Domain.Timesheets;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Persistence.ApplicationDBContext;

namespace Infrastructure.Persistence
{
    internal class TimesheetRepository : EFRepository<Timesheet, int>, IRepository<Timesheet, int>, ITimesheetRepository
    {
        public TimesheetRepository(ApplicationDBContext context) : base(context)
        {
        }

        public string GetNewTimesheetNumber()
        {
            try
            {
                var sqlQuery = @"
                    SELECT IFNULL(MAX(CAST(SUBSTR(TimesheetNumber, 1, 6) AS INTEGER)) + 1, 1) AS Value 
                    FROM Timesheet
                    WHERE TimesheetNumber GLOB '[0-9][0-9][0-9][0-9][0-9][0-9][A-Z][A-Z][0-9][0-9]'";

                var x = _context.Set<ValReturn<int>>()
                                .FromSqlRaw(sqlQuery)
                                .AsEnumerable()
                                .First().Value;

                return $"{x:000000}US01";
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
    }
}