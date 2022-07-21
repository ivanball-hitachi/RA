using Domain.Common;

namespace Domain.Main;
public class LineProperty : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}