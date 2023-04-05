using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public class ApprovalStatus : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}