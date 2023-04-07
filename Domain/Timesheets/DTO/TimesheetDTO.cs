using Domain.Common;
using System.Globalization;

namespace Domain.Timesheets.DTO
{
    public partial class TimesheetBaseDTO : AuditableDTO
    {
        //public string TimesheetNumber { get; set; } = default!;
        //public int EmployeeId { get; set; } = default;
        //public DateTime PeriodStartDate { get; set; } = default;
        //public DateTime PeriodEndDate { get; set; } = default;
        public int ApprovalStatusId { get; set; } = 1;  // Initialize as "Draft" (ApprovalStatus.Id = 1 ?)
        //public double TotalHours { get; set; } = 0;
        public bool IsReadOnly
        {
            get
            {
                return (ApprovalStatusId != 1); // TODO: Check for "Draft" (ApprovalStatus.Id = 1 ?)
            }
        }
    }

    //public class TimesheetDTO : TimesheetBaseDTO, IBaseEntity<int>
    //{
    //    public int Id { get; set; }
    //    public string? EmployeeFullName { get; set; }
    //    public string? ApprovalStatusName { get; set; }
    //}

    //public class TimesheetForCreationDTO : TimesheetBaseDTO
    //{
    //}

    //public class TimesheetForUpdateDTO : TimesheetBaseDTO, IBaseEntity<int>
    //{
    //    public int Id { get; set; }
    //}
}