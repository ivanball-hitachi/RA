using Domain.Common;

namespace Domain.Main;
public class Category : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}