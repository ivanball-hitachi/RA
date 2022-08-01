using Domain.Main;
using Domain.Timesheets;

namespace Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Activity, int> ActivityRepository { get; }
        IRepository<ApprovalStatus, int> ApprovalStatusRepository { get; }
        IRepository<Category, int> CategoryRepository { get; }
        IRepository<Customer, int> CustomerRepository { get; }
        IRepository<Employee, int> EmployeeRepository { get; }
        IRepository<Employee_Reviewer, int> Employee_ReviewerRepository { get; }
        IRepository<EmployeeType, int> EmployeeTypeRepository { get; }
        IRepository<LegalEntity, int> LegalEntityRepository { get; }
        IRepository<LineProperty, int> LinePropertyRepository { get; }
        IRepository<Location, int> LocationRepository { get; }
        IRepository<Project, int> ProjectRepository { get; }
        ITimesheetRepository TimesheetRepository { get; }
        IRepository<TimesheetLine, int> TimesheetLineRepository { get; }
        IRepository<TimesheetLineDetail, int> TimesheetLineDetailRepository { get; }

        Task<int> SaveAsync();
        int Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}