using Domain.Common;

namespace Domain.Main;
public class Project : AuditableWithBaseEntity<int>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}