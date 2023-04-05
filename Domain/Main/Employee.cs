using Domain.Common;
using Generators;

namespace Domain.Main;

[GenerateDTOClass]
public partial class Employee : AuditableWithBaseEntity<int>
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    [GenerateInDTOClass(PropertyName = "EmployeeTypeName", DataType = "string?")]
    public EmployeeType? EmployeeType { get; set; }
    public int EmployeeTypeId { get; set; }

    public string? UserId { get; set; } = default;

    [ExcludeFromCodeGeneration]
    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
}