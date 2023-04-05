using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class EmployeeType : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}