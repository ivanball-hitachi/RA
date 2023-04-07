using Domain.Common;
using Domain.Main;
using Generators;

namespace Domain.Timesheets;

[GenerateDTOClass]
public class TimesheetLine : AuditableWithBaseEntity<int>
{
    [ExcludeFromCodeGeneration]
    public Timesheet Timesheet { get; set; } = default!;
    public int TimesheetId { get; set; }

    [GenerateInDTOClass(PropertyName = "LegalEntityName", DataType = "string?")]
    public LegalEntity? LegalEntity { get; set; }
    public int LegalEntityId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "LocationName", DataType = "string?")]
    public Location? Location { get; set; }
    public int LocationId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "CustomerName", DataType = "string?")]
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "ProjectName", DataType = "string?")]
    public Project? Project { get; set; }
    public int ProjectId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "ActivityName", DataType = "string?")]
    public Activity? Activity { get; set; }
    public int ActivityId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "CategoryName", DataType = "string?")]
    public Category? Category { get; set; }
    public int CategoryId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "LinePropertyName", DataType = "string?")]
    public LineProperty? LineProperty { get; set; }
    public int LinePropertyId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "ApprovalStatusName", DataType = "string?")]
    public ApprovalStatus? ApprovalStatus { get; set; }
    [ExcludeFromCodeGeneration]
    public int ApprovalStatusId { get; set; } = default;

    [ExcludeFromCodeGeneration]
    public List<TimesheetLineDetail> TimesheetLineDetails { get; set; } = new();
}