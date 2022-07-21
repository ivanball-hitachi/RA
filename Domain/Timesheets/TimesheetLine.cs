using Domain.Common;
using Domain.Main;

namespace Domain.Timesheets;
public class TimesheetLine : AuditableWithBaseEntity<int>
{
    public Timesheet Timesheet { get; set; } = default!;
    public int TimesheetId { get; set; }

    public LegalEntity? LegalEntity { get; set; }
    public int LegalEntityId { get; set; } = default;

    public Location? Location { get; set; }
    public int LocationId { get; set; } = default;

    public Customer? Customer { get; set; }
    public int CustomerId { get; set; } = default;

    public Project? Project { get; set; }
    public int ProjectId { get; set; } = default;

    public Activity? Activity { get; set; }
    public int ActivityId { get; set; } = default;

    public Category? Category { get; set; }
    public int CategoryId { get; set; } = default;

    public LineProperty? LineProperty { get; set; }
    public int LinePropertyId { get; set; } = default;

    public List<TimesheetLineDetail> TimesheetLineDetails { get; set; } = new();
}