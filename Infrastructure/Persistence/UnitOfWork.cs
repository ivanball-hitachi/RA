using Application.Common.Interface;
using Domain.Main;
using Domain.Timesheets;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly ApplicationDBContext _context;
        IDbContextTransaction? dbContextTransaction;

        private IRepository<Activity, int>? _activityRepository;
        private IRepository<ApprovalStatus, int>? _approvalStatusRepository;
        private IRepository<Category, int>? _categoryRepository;
        private IRepository<Customer, int>? _customerRepository;
        private IRepository<Employee, int>? _employeeRepository;
        private IRepository<EmployeeType, int>? _employeeTypeRepository;
        private IRepository<LegalEntity, int>? _legalEntityRepository;
        private IRepository<LineProperty, int>? _linePropertyRepository;
        private IRepository<Location, int>? _locationRepository;
        private IRepository<Project, int>? _projectRepository;
        private ITimesheetRepository? _timesheetRepository;
        private IRepository<TimesheetLine, int>? _timesheetLineRepository;
        private IRepository<TimesheetLineDetail, int>? _timesheetLineDetailRepository;
        #endregion

        #region Ctor
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Repository
        public IRepository<Activity, int> ActivityRepository
        {
            get
            {
                if (_activityRepository is null)
                    _activityRepository = new EFRepository<Activity, int>(_context);
                return _activityRepository;
            }
        }

        public IRepository<ApprovalStatus, int> ApprovalStatusRepository
        {
            get
            {
                if (_approvalStatusRepository is null)
                    _approvalStatusRepository = new EFRepository<ApprovalStatus, int>(_context);
                return _approvalStatusRepository;
            }
        }

        public IRepository<Category, int> CategoryRepository
        {
            get
            {
                if (_categoryRepository is null)
                    _categoryRepository = new EFRepository<Category, int>(_context);
                return _categoryRepository;
            }
        }

        public IRepository<Customer, int> CustomerRepository
        {
            get
            {
                if (_customerRepository is null)
                    _customerRepository = new EFRepository<Customer, int>(_context);
                return _customerRepository;
            }
        }

        public IRepository<Employee, int> EmployeeRepository
        {
            get
            {
                if (_employeeRepository is null)
                    _employeeRepository = new EFRepository<Employee, int>(_context);
                return _employeeRepository;
            }
        }

        public IRepository<EmployeeType, int> EmployeeTypeRepository
        {
            get
            {
                if (_employeeTypeRepository is null)
                    _employeeTypeRepository = new EFRepository<EmployeeType, int>(_context);
                return _employeeTypeRepository;
            }
        }

        public IRepository<LegalEntity, int> LegalEntityRepository
        {
            get
            {
                if (_legalEntityRepository is null)
                    _legalEntityRepository = new EFRepository<LegalEntity, int>(_context);
                return _legalEntityRepository;
            }
        }

        public IRepository<LineProperty, int> LinePropertyRepository
        {
            get
            {
                if (_linePropertyRepository is null)
                    _linePropertyRepository = new EFRepository<LineProperty, int>(_context);
                return _linePropertyRepository;
            }
        }

        public IRepository<Location, int> LocationRepository
        {
            get
            {
                if (_locationRepository is null)
                    _locationRepository = new EFRepository<Location, int>(_context);
                return _locationRepository;
            }
        }

        public IRepository<Project, int> ProjectRepository
        {
            get
            {
                if (_projectRepository is null)
                    _projectRepository = new EFRepository<Project, int>(_context);
                return _projectRepository;
            }
        }

        public ITimesheetRepository TimesheetRepository
        {
            get
            {
                if (_timesheetRepository is null)
                    _timesheetRepository = new TimesheetRepository(_context);
                return _timesheetRepository;
            }
        }

        public IRepository<TimesheetLine, int> TimesheetLineRepository
        {
            get
            {
                if (_timesheetLineRepository is null)
                    _timesheetLineRepository = new EFRepository<TimesheetLine, int>(_context);
                return _timesheetLineRepository;
            }
        }

        public IRepository<TimesheetLineDetail, int> TimesheetLineDetailRepository
        {
            get
            {
                if (_timesheetLineDetailRepository is null)
                    _timesheetLineDetailRepository = new EFRepository<TimesheetLineDetail, int>(_context);
                return _timesheetLineDetailRepository;
            }
        }
        #endregion

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public int Save()
        {
            return _context.SaveChanges();
        }
        
        public void BeginTransaction()
        {
            dbContextTransaction = _context.Database.BeginTransaction();
        }
        
        public void CommitTransaction()
        {
            if (dbContextTransaction is not null)
            {
                dbContextTransaction.Commit();
            }
        }
        
        public void RollbackTransaction()
        {
            if (dbContextTransaction is not null)
            {
                dbContextTransaction.Rollback();
            }
        }
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}