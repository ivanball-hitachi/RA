using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Employee_Reviewer : AuditableWithBaseEntity<int>
{
    [GenerateInDTOClass(PropertyName = "EmployeeFullName", DataType = "string?")]
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; } = default;

    [GenerateInDTOClass(PropertyName = "ReviewerFullName", DataType = "string?")]
    public Employee? Reviewer { get; set; }
    public int ReviewerId { get; set; } = default;
}