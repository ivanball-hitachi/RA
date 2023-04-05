using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class Customer : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string CustomerAccount { get; set; } = default!;
    public string? AccountNumber { get; set; } = default!;
}