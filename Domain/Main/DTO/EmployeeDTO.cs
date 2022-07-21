using Domain.Common;

namespace Domain.Main.DTO
{
    public class EmployeeBaseDTO : AuditableDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int EmployeeTypeId { get; set; } = default;
    }

    public class EmployeeDTO : EmployeeBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
        public string? EmployeeTypeName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }

    public class EmployeeForCreationDTO : EmployeeBaseDTO
    {
    }

    public class EmployeeForUpdateDTO : EmployeeBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}