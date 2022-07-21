using Domain.Common;
using Domain.Main;

namespace Domain.Timesheets;
public class Timesheet : AuditableWithBaseEntity<int>
{
    public string TimesheetNumber { get; set; } = default!;

    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; } = default;

    public DateTime PeriodStartDate { get; set; }

    public DateTime PeriodEndDate { get; set; }

    public ApprovalStatus? ApprovalStatus { get; set; }
    public int ApprovalStatusId { get; set; } = default;

    public List<TimesheetLine> TimesheetLines { get; set; } = new();

    public double TotalHours
    {
        get
        {
            double totalHours = 0;
            foreach (TimesheetLine timesheetLine in TimesheetLines)
            {
                foreach (TimesheetLineDetail timesheetLineDetail in timesheetLine.TimesheetLineDetails)
                {
                    totalHours += timesheetLineDetail.Hours;
                }
            }
            return totalHours;
        }
        set { }
    }
}