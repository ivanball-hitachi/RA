using Domain.Common;

namespace Domain.Timesheets.DTO
{
    public class TimesheetLineDetailBaseDTO : AuditableDTO
    {
        public int TimesheetLineId { get; set; }

        public DateTime Day { get; set; }

        public float Hours { get; set; } = default;

        public string? InternalComment { get; set; }

        public string? ExternalComment { get; set; }
    }

    public class TimesheetLineDetailDTO : TimesheetLineDetailBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }

    public class TimesheetLineDetailForCreationDTO : TimesheetLineDetailBaseDTO
    {
    }

    public class TimesheetLineDetailForUpdateDTO : TimesheetLineDetailBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}