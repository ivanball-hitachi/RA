using Domain.Common;

namespace Domain.Main;
public class ApprovalStatus : AuditableWithBaseEntity<int>
{
    public string Name { get; set; } = default!;
}