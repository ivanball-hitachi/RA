using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class LineProperty : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}