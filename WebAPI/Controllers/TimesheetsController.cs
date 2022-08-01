using Microsoft.AspNetCore.Mvc;
using Domain.Timesheets;
using AutoMapper;
using Domain.Timesheets.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/timesheets")]
    public class TimesheetsController : EntityControllerBase<Timesheet, TimesheetDTO, TimesheetForCreationDTO, TimesheetForUpdateDTO, int>
    {
        protected readonly new ITimesheetService _entityService;
        public TimesheetsController(ITimesheetService entityService, ILogger<EntityControllerBase<Timesheet, TimesheetDTO, TimesheetForCreationDTO, TimesheetForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Timesheet")
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        [HttpGet("getperiodstartdate")]
        public ActionResult<DateTime> GetPeriodStartDate(DateTime periodDate)
        {
            // Monday of the current week
            var firstDayOfWeek = periodDate.AddDays(DayOfWeek.Sunday - (periodDate.AddDays(-1)).DayOfWeek).Date;
            // First day of the current month
            var firstDayOfMonth = periodDate.AddDays(1 - (periodDate.Day)).Date;
            // Return the latest of both
            return Ok((firstDayOfWeek > firstDayOfMonth) ? firstDayOfWeek : firstDayOfMonth);
        }

        [HttpGet("getperiodenddate")]
        public ActionResult<DateTime> GetPeriodEndDate(DateTime periodDate)
        {
            // Sunday of the current week
            var lastDayOfWeek = periodDate.AddDays(DayOfWeek.Sunday - (periodDate.AddDays(-1)).DayOfWeek).AddDays(6).Date;
            // Last day of the current month
            var lastDayOfMonth = periodDate.AddDays(1 - (periodDate.Day)).AddMonths(1).AddDays(-1).Date;
            // Return the earliest of both
            return Ok((lastDayOfWeek < lastDayOfMonth) ? lastDayOfWeek : lastDayOfMonth);
        }

        [HttpGet("getnewtimesheetnumber")]
        public ActionResult<string> GetNewTimesheetNumber()
        {
            var newTimesheetNumber = _entityService.GetNewTimesheetNumber();

            return Ok(newTimesheetNumber);
        }

        [HttpGet("paged")]
        public override async Task<ActionResult<IEnumerable<TimesheetDTO>>> GetAll(
            string? searchValue, int pageNumber = 1, int pageSize = 10, bool includeChildren = true)
        {
            return await base.GetAll(searchValue, pageNumber, pageSize, includeChildren);
        }

        [HttpGet("{id}", Name = "GetTimesheet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<TimesheetDTO>> GetById(int id, bool includeChildren = true)
        {
            return await base.GetById(id, includeChildren);
        }

        [HttpPost]
        public override async Task<ActionResult<TimesheetDTO>> Create(TimesheetForCreationDTO entityDTO, string action = default!)
        {
            switch (action)
            {
                case "Submit":
                        entityDTO.ApprovalStatusId = 2;     // TODO: Get ApprovalStatusId corresponding to "In review"
                        break;
                case "Approve":
                        entityDTO.ApprovalStatusId = 3;     // TODO: Get ApprovalStatusId corresponding to "Approved"
                        break;
                case "Reject":
                        entityDTO.ApprovalStatusId = 4;     // TODO: Get ApprovalStatusId corresponding to "Rejected"
                        break;
            }

            return await base.Create(entityDTO);
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> Update(int id, TimesheetForUpdateDTO entityDTO, string action = default!)
        {
            switch (action)
            {
                case "Submit":
                    entityDTO.ApprovalStatusId = 2;     // TODO: Get ApprovalStatusId corresponding to "In review"
                    break;
                case "Approve":
                    entityDTO.ApprovalStatusId = 3;     // TODO: Get ApprovalStatusId corresponding to "Approved"
                    break;
                case "Reject":
                    entityDTO.ApprovalStatusId = 4;     // TODO: Get ApprovalStatusId corresponding to "Rejected"
                    break;
            }

            return await base.Update(id, entityDTO);
        }


    }
}