using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Location : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string CountryRegion { get; set; } = default!;
}