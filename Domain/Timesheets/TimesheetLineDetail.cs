using Domain.Common;

namespace Domain.Timesheets;
public class TimesheetLineDetail : AuditableWithBaseEntity<int>
{
    public int TimesheetLineId { get; set; }

    public DateTime Day { get; set; }

    public float Hours { get; set; } = default;

    public string? InternalComment { get; set; }

    public string? ExternalComment { get; set; }
}