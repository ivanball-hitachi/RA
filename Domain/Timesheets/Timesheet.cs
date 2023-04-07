using Domain.Common;
using Domain.Main;
using Generators;

namespace Domain.Timesheets;

[GenerateDTOClass]
public class Timesheet : AuditableWithBaseEntity<int>
{
    public string TimesheetNumber { get; set; } = default!;

    [GenerateInDTOClass(PropertyName = "EmployeeFullName", DataType = "string?")]
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; } = default;

    public DateTime PeriodStartDate { get; set; }

    public DateTime PeriodEndDate { get; set; }

    [GenerateInDTOClass(PropertyName = "ApprovalStatusName", DataType = "string?")]
    public ApprovalStatus? ApprovalStatus { get; set; }
    [ExcludeFromCodeGeneration]
    public int ApprovalStatusId { get; set; } = default;

    [ExcludeFromCodeGeneration]
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