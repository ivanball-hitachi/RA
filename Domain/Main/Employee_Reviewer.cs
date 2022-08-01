using Domain.Common;

namespace Domain.Main;
public class Employee_Reviewer : AuditableWithBaseEntity<int>
{
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; } = default;

    public Employee? Reviewer { get; set; }
    public int ReviewerId { get; set; } = default;
}