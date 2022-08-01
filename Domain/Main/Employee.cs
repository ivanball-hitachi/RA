using Domain.Common;

namespace Domain.Main;
public class Employee : AuditableWithBaseEntity<int>
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public EmployeeType? EmployeeType { get; set; }
    public int EmployeeTypeId { get; set; }

    public string? UserId { get; set; } = default;

    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
}