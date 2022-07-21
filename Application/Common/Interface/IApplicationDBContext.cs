using Domain.Main;
using Domain.Timesheets;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IApplicationDBContext
    {
        DbSet<Activity> Activities { get; set; }
        DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeType> EmployeeTypes { get; set; }
        DbSet<LegalEntity> LegalEntities { get; set; }
        DbSet<LineProperty> LineProperties { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Timesheet> Timesheets { get; set; }
        DbSet<TimesheetLine> TimesheetLines { get; set; }
        DbSet<TimesheetLineDetail> TimesheetLineDetails { get; set; }


        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}