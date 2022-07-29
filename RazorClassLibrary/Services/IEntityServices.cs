using Domain.Main.DTO;
using Domain.Timesheets.DTO;

namespace RazorClassLibrary.Services
{
    public interface IEntityServices
    {
        IEntityService<ActivityDTO, ActivityForCreationDTO, ActivityForUpdateDTO, int> ActivityService { get; }
        IEntityService<ApprovalStatusDTO, ApprovalStatusForCreationDTO, ApprovalStatusForUpdateDTO, int> ApprovalStatusService { get; }
        IEntityService<CategoryDTO, CategoryForCreationDTO, CategoryForUpdateDTO, int> CategoryService { get; }
        IEntityService<CustomerDTO, CustomerForCreationDTO, CustomerForUpdateDTO, int> CustomerService { get; }
        IEntityService<EmployeeDTO, EmployeeForCreationDTO, EmployeeForUpdateDTO, int> EmployeeService { get; }
        IEntityService<EmployeeTypeDTO, EmployeeTypeForCreationDTO, EmployeeTypeForUpdateDTO, int> EmployeeTypeService { get; }
        IEntityService<LegalEntityDTO, LegalEntityForCreationDTO, LegalEntityForUpdateDTO, int> LegalEntityService { get; }
        IEntityService<LinePropertyDTO, LinePropertyForCreationDTO, LinePropertyForUpdateDTO, int> LinePropertyService { get; }
        IEntityService<LocationDTO, LocationForCreationDTO, LocationForUpdateDTO, int> LocationService { get; }
        IEntityService<ProjectDTO, ProjectForCreationDTO, ProjectForUpdateDTO, int> ProjectService { get; }
        ITimesheetService TimesheetService { get; }
        ITimesheetLineService TimesheetLineService { get; }
    }
}