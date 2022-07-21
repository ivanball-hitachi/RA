using Domain.Common;

namespace Domain.Main;
public class EmployeeType : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}