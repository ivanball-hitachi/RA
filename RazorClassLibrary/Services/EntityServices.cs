using Domain.Main.DTO;
using Domain.Timesheets.DTO;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary.Services
{
    public class EntityServices : IEntityServices
    {
        #region Properties
        private readonly string _baseAddress;
        private readonly IIdentityService _identityService;

        private IEntityService<ActivityDTO,         ActivityForCreationDTO,         ActivityForUpdateDTO,       int>? _activityService;
        private IEntityService<ApprovalStatusDTO,   ApprovalStatusForCreationDTO,   ApprovalStatusForUpdateDTO, int>? _approvalStatusService;
        private IEntityService<CategoryDTO,         CategoryForCreationDTO,         CategoryForUpdateDTO,       int>? _categoryService;
        private IEntityService<CustomerDTO,         CustomerForCreationDTO,         CustomerForUpdateDTO,       int>? _customerService;
        private IEntityService<EmployeeDTO,         EmployeeForCreationDTO,         EmployeeForUpdateDTO,       int>? _employeeService;
        private IEntityService<EmployeeTypeDTO,     EmployeeTypeForCreationDTO,     EmployeeTypeForUpdateDTO,   int>? _employeeTypeService;
        private IEntityService<LegalEntityDTO,      LegalEntityForCreationDTO,      LegalEntityForUpdateDTO,    int>? _legalEntityService;
        private IEntityService<LinePropertyDTO,     LinePropertyForCreationDTO,     LinePropertyForUpdateDTO,   int>? _linePropertyService;
        private IEntityService<LocationDTO,         LocationForCreationDTO,         LocationForUpdateDTO,       int>? _locationService;
        private IEntityService<ProjectDTO,          ProjectForCreationDTO,          ProjectForUpdateDTO,        int>? _projectService;
        private ITimesheetService? _timesheetService;
        private ITimesheetLineService? _timesheetLineService;
        #endregion

        #region Ctor
        public EntityServices(IConfiguration configuration, IIdentityService identityService)
        {
            _baseAddress = (configuration is not null ? configuration["APIBaseURL"] : throw new ArgumentNullException(nameof(configuration)));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }
        #endregion

        #region Services
        public IEntityService<ActivityDTO, ActivityForCreationDTO, ActivityForUpdateDTO, int> ActivityService
        {
            get
            {
                if (_activityService is null)
                    _activityService = new EntityService<ActivityDTO, ActivityForCreationDTO, ActivityForUpdateDTO, int>(_baseAddress, "api/activities", _identityService);
                return _activityService;
            }
        }

        public IEntityService<ApprovalStatusDTO, ApprovalStatusForCreationDTO, ApprovalStatusForUpdateDTO, int> ApprovalStatusService
        {
            get
            {
                if (_approvalStatusService is null)
                    _approvalStatusService = new EntityService<ApprovalStatusDTO, ApprovalStatusForCreationDTO, ApprovalStatusForUpdateDTO, int>(_baseAddress, "api/approvalstatuses", _identityService);
                return _approvalStatusService;
            }
        }

        public IEntityService<CategoryDTO, CategoryForCreationDTO, CategoryForUpdateDTO, int> CategoryService
        {
            get
            {
                if (_categoryService is null)
                    _categoryService = new EntityService<CategoryDTO, CategoryForCreationDTO, CategoryForUpdateDTO, int>(_baseAddress, "api/categories", _identityService);
                return _categoryService;
            }
        }

        public IEntityService<CustomerDTO, CustomerForCreationDTO, CustomerForUpdateDTO, int> CustomerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new EntityService<CustomerDTO, CustomerForCreationDTO, CustomerForUpdateDTO, int>(_baseAddress, "api/customers", _identityService);
                return _customerService;
            }
        }

        public IEntityService<EmployeeDTO, EmployeeForCreationDTO, EmployeeForUpdateDTO, int> EmployeeService
        {
            get
            {
                if (_employeeService is null)
                    _employeeService = new EntityService<EmployeeDTO, EmployeeForCreationDTO, EmployeeForUpdateDTO, int>(_baseAddress, "api/employees", _identityService);
                return _employeeService;
            }
        }

        public IEntityService<EmployeeTypeDTO, EmployeeTypeForCreationDTO, EmployeeTypeForUpdateDTO, int> EmployeeTypeService
        {
            get
            {
                if (_employeeTypeService is null)
                    _employeeTypeService = new EntityService<EmployeeTypeDTO, EmployeeTypeForCreationDTO, EmployeeTypeForUpdateDTO, int>(_baseAddress, "api/employeetypes", _identityService);
                return _employeeTypeService;
            }
        }

        public IEntityService<LegalEntityDTO, LegalEntityForCreationDTO, LegalEntityForUpdateDTO, int> LegalEntityService
        {
            get
            {
                if (_legalEntityService is null)
                    _legalEntityService = new EntityService<LegalEntityDTO, LegalEntityForCreationDTO, LegalEntityForUpdateDTO, int>(_baseAddress, "api/legalentities", _identityService);
                return _legalEntityService;
            }
        }

        public IEntityService<LinePropertyDTO, LinePropertyForCreationDTO, LinePropertyForUpdateDTO, int> LinePropertyService
        {
            get
            {
                if (_linePropertyService is null)
                    _linePropertyService = new EntityService<LinePropertyDTO, LinePropertyForCreationDTO, LinePropertyForUpdateDTO, int>(_baseAddress, "api/lineproperties", _identityService);
                return _linePropertyService;
            }
        }

        public IEntityService<LocationDTO, LocationForCreationDTO, LocationForUpdateDTO, int> LocationService
        {
            get
            {
                if (_locationService is null)
                    _locationService = new EntityService<LocationDTO, LocationForCreationDTO, LocationForUpdateDTO, int>(_baseAddress, "api/locations", _identityService);
                return _locationService;
            }
        }

        public IEntityService<ProjectDTO, ProjectForCreationDTO, ProjectForUpdateDTO, int> ProjectService
        {
            get
            {
                if (_projectService is null)
                    _projectService = new EntityService<ProjectDTO, ProjectForCreationDTO, ProjectForUpdateDTO, int>(_baseAddress, "api/projects", _identityService);
                return _projectService;
            }
        }

        public ITimesheetService TimesheetService
        {
            get
            {
                if (_timesheetService is null)
                    _timesheetService = new TimesheetService(_baseAddress, "api/timesheets", _identityService);
                return _timesheetService;
            }
        }

        public ITimesheetLineService TimesheetLineService
        {
            get
            {
                if (_timesheetLineService is null)
                    _timesheetLineService = new TimesheetLineService(_baseAddress, "api/timesheetlines", _identityService);
                return _timesheetLineService;
            }
        }
        #endregion
    }
}