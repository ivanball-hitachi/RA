using Domain.Common;

namespace Domain.Main;
public class Location : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string CountryRegion { get; set; } = default!;
}