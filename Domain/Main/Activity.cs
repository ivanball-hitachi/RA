using Domain.Common;

namespace Domain.Main;
public class Activity : AuditableWithBaseEntity<int>
{
    public string Number { get; set; } = default!;
    public string Name { get; set; } = default!;
}