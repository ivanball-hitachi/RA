using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Project : AuditableWithBaseEntity<int>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}