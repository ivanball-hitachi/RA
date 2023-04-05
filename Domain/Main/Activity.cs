using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Activity : AuditableWithBaseEntity<int>
{
    public string Number { get; set; } = default!;
    public string Name { get; set; } = default!;
}