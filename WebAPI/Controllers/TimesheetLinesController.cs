using Microsoft.AspNetCore.Mvc;
using Domain.Timesheets;
using AutoMapper;
using Domain.Timesheets.DTO;
using System.Text.Json;
using Application.Common.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/timesheets/{timesheetId}/timesheetlines")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TimesheetLinesController : ControllerBase
    {
        private readonly ITimesheetService _entityService;
        private readonly ILogger<TimesheetLinesController> _logger;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;

        public TimesheetLinesController(ITimesheetService entityService,
            ILogger<TimesheetLinesController> logger, IMapper mapper)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<TimesheetLineDTO>>> GetAll(int timesheetId,
            string? searchValue, int pageNumber = 1, int pageSize = 10, bool includeChildren = true)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                _logger.LogInformation(
                    $"Timesheet with id {timesheetId} wasn't found when accessing TimesheetLines.");
                return NotFound();
            }

            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (entities, paginationMetadata) = await _entityService
                .GetTimesheetLinesAsync(timesheetId, searchValue, pageNumber, pageSize, true, includeChildren);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<TimesheetLineDTO>>(entities));
        }

        [HttpGet("{id}", Name = "GetTimesheetLine")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TimesheetLineDTO>> GetById(int timesheetId, int id, string? searchValue = default, bool includeChildren = true)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                _logger.LogInformation(
                    $"Timesheet with id {timesheetId} wasn't found when accessing TimesheetLines.");
                return NotFound();
            }

            var entity = await _entityService.GetTimesheetLineByIdAsync(timesheetId, id, true, includeChildren);
            if (entity is null)
            {
                _logger.LogInformation(
                    $"TimesheetLine with Id {id} wasn't found.");
                return NotFound();
            }

            var timesheet = await _entityService.GetByIdAsync(timesheetId);
            var timesheetLineDTO = _mapper.Map<TimesheetLineDTO>(entity);
            timesheetLineDTO.PeriodStartDate = timesheet.PeriodStartDate;
            timesheetLineDTO.PeriodEndDate = timesheet.PeriodEndDate;

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                timesheetLineDTO.TimesheetLineDetails = timesheetLineDTO.TimesheetLineDetails.FindAll(tsld => 
                    tsld.InternalComment!.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase) ||
                    tsld.ExternalComment!.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase));
            }

            return Ok(timesheetLineDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TimesheetLineDTO>> Create(int timesheetId, TimesheetLineForCreationDTO entityDTO)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<TimesheetLine>(entityDTO);

            await _entityService.AddTimesheetLineAsync(timesheetId, entity);

            var timesheet = await _entityService.GetByIdAsync(timesheetId);
            var createdEntityToReturn = _mapper.Map<TimesheetLineDTO>(entity);
            createdEntityToReturn.PeriodStartDate = timesheet.PeriodStartDate;
            createdEntityToReturn.PeriodEndDate = timesheet.PeriodEndDate;

            return CreatedAtRoute("GetTimesheetLine",
                 new
                 {
                     timesheetId = timesheetId,
                     id = createdEntityToReturn.Id
                 },
                 createdEntityToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int timesheetId, int id,
            TimesheetLineForUpdateDTO entityDTO)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                return NotFound();
            }

            var entity = await _entityService.GetTimesheetLineByIdAsync(timesheetId, id);
            if (entity is null)
            {
                return NotFound();
            }

            await _entityService.UpdateTimesheetLineAsync(timesheetId, _mapper.Map(entityDTO, entity));

            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdate(int timesheetId, int id,
            JsonPatchDocument<TimesheetLineForUpdateDTO> patchDocument)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                return NotFound();
            }

            var entity = await _entityService.GetTimesheetLineByIdAsync(timesheetId, id);
            if (entity is null)
            {
                return NotFound();
            }

            var entityToPatch = _mapper.Map<TimesheetLineForUpdateDTO>(entity);

            patchDocument.ApplyTo(entityToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(entityToPatch))
            {
                return BadRequest(ModelState);
            }

            await _entityService.UpdateTimesheetLineAsync(timesheetId, _mapper.Map(entityToPatch, entity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int timesheetId, int id)
        {
            if (!await _entityService.ExistsAsync(timesheetId))
            {
                return NotFound();
            }

            var entity = await _entityService.GetTimesheetLineByIdAsync(timesheetId, id);
            if (entity is null)
            {
                return NotFound();
            }

            var isDeleted = await _entityService.DeleteTimesheetLineAsync(entity);

            return NoContent();
        }
    }
}