using Domain.Common;

namespace Domain.Main.DTO
{
    public class Employee_ReviewerBaseDTO : AuditableDTO
    {
        public int EmployeeId { get; set; } = default;
        public int ReviewerId { get; set; } = default;
    }

    public class Employee_ReviewerDTO : Employee_ReviewerBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? ReviewerFullName { get; set; }
    }

    public class Employee_ReviewerForCreationDTO : Employee_ReviewerBaseDTO
    {
    }

    public class Employee_ReviewerForUpdateDTO : Employee_ReviewerBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}