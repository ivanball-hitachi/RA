using Domain.Common;

namespace Domain.Timesheets.DTO
{
    public class TimesheetLineBaseDTO : AuditableDTO
    {
        public int TimesheetId { get; set; }
        public int LegalEntityId { get; set; } = default;
        public int LocationId { get; set; } = default;
        public int CustomerId { get; set; } = default;
        public int ProjectId { get; set; } = default;
        public int ActivityId { get; set; } = default;
        public int CategoryId { get; set; } = default;
        public int LinePropertyId { get; set; } = default;
        public int ApprovalStatusId { get; set; } = 1;  // Initialize as "Draft" (ApprovalStatus.Id = 1 ?)

        public List<TimesheetLineDetailDTO> TimesheetLineDetails { get; set; } = new();
    }

    public class TimesheetLineDTO : TimesheetLineBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
        public string? LegalEntityName { get; set; }
        public string? LocationName { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectName { get; set; }
        public string? ActivityName { get; set; }
        public string? CategoryName { get; set; }
        public string? LinePropertyName { get; set; }
        public string? ApprovalStatusName { get; set; }
        public string? EmployeeTypeName { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
    }

    public class TimesheetLineForCreationDTO : TimesheetLineBaseDTO
    {
    }

    public class TimesheetLineForUpdateDTO : TimesheetLineBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}