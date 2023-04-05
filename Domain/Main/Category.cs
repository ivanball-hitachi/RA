using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Category : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}