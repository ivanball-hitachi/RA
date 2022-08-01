using Application.Common.Interface;
using Domain.Common;
using Domain.Main;
using Domain.Timesheets;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {

        #region Properties
        private readonly DateTime _currentDateTime;
        #endregion

        #region Ctor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {
            _currentDateTime = DateTime.Now;
        }
        #endregion

        public class ValReturn<T>
        {
            public T Value { get; set; } = default!;
        }


        public DbSet<Activity> Activities { get; set; } = null!;
        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Employee_Reviewer> Employee_Reviewers { get; set; } = null!;
        public DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
        public DbSet<LegalEntity> LegalEntities { get; set; } = null!;
        public DbSet<LineProperty> LineProperties { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Timesheet> Timesheets { get; set; } = null!;
        public DbSet<TimesheetLine> TimesheetLines { get; set; } = null!;
        public DbSet<TimesheetLineDetail> TimesheetLineDetails { get; set; } = null!;

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = 1; //Get Current UserID
                        entry.Entity.CreatedOn = _currentDateTime;
                        entry.Entity.LastModifiedBy = 1; //Get Current UserID
                        entry.Entity.LastModifiedOn = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = 1; //Get Current UserID
                        entry.Entity.LastModifiedOn = _currentDateTime;
                        break;
                }
            }
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            modelBuilder.Entity<ValReturn<bool>>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValReturn<int>>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValReturn<DateTime>>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValReturn<string>>().HasNoKey().ToView(null);

            modelBuilder.Entity<Employee>()
                .HasData(
               new Employee()
               {
                   Id = 1,
                   FirstName = "Ivan",
                   LastName = "Ball-llovera",
                   EmployeeTypeId = 1
               },
               new Employee()
               {
                   Id = 2,
                   FirstName = "Ursula",
                   LastName = "Conley",
                   EmployeeTypeId = 1
               });

            modelBuilder.Entity<Employee_Reviewer>()
                .HasData(
               new Employee_Reviewer()
               {
                   Id = 1,
                   EmployeeId = 1,
                   ReviewerId = 2
               },
               new Employee_Reviewer()
               {
                   Id = 2,
                   EmployeeId = 2,
                   ReviewerId = 2
               });

            modelBuilder.Entity<EmployeeType>()
                .HasData(
               new EmployeeType()
               {
                   Id = 1,
                   Name = "Onshore"
               },
               new EmployeeType()
               {
                   Id = 2,
                   Name = "Offshore"
               });

            modelBuilder.Entity<ApprovalStatus>()
                .HasData(
               new ApprovalStatus()
               {
                   Id = 1,
                   Name = "Draft"
               },
               new ApprovalStatus()
               {
                   Id = 2,
                   Name = "In review"
               },
               new ApprovalStatus()
               {
                   Id = 3,
                   Name = "Approved"
               },
               new ApprovalStatus()
               {
                   Id = 4,
                   Name = "Rejected"
               },
               new ApprovalStatus()
               {
                   Id = 5,
                   Name = "Posted"
               });

            modelBuilder.Entity<LegalEntity>()
                .HasData(
               new LegalEntity()
               {
                   Id = 1,
                   Code = "US01",
                   Name = "Hitachi Solutions America Ltd"
               },
               new LegalEntity()
               {
                   Id = 2,
                   Code = "CA01",
                   Name = "Hitachi Solutions Canada Ltd"
               },
                new LegalEntity()
                {
                    Id = 3,
                    Code = "DE01",
                    Name = "Hitachi Solutions Germany GmbH"
                },
               new LegalEntity()
               {
                   Id = 4,
                   Code = "GB01",
                   Name = "Hitachi Solutions Europe Ltd"
               },
               new LegalEntity()
               {
                   Id = 5,
                   Code = "SG01",
                   Name = "Hitachi Solutions Asia Pacific Pte..."
               });

            modelBuilder.Entity<Location>()
                .HasData(
               new Location()
               {
                   Id = 1,
                   Name = "United States",
                   CountryRegion = "USA"
               },
               new Location()
               {
                   Id = 2,
                   Name = "Canada",
                   CountryRegion = "CAN"
               });

            modelBuilder.Entity<Customer>()
                .HasData(
               new Customer()
               {
                   Id = 1,
                   Name = "Hitachi Solutions America, Ltd.",
                   CustomerAccount = "C00085-US01"
               },
               new Customer()
               {
                   Id = 2,
                   Name = "Hitachi Vantara INC",
                   CustomerAccount = "C00014-US01"
               });

            modelBuilder.Entity<Project>()
                .HasData(
               new Project()
               {
                   Id = 1,
                   Code = "000000.02.01",
                   Name = "BSG Internal - Time Off"
               },
               new Project()
               {
                   Id = 2,
                   Code = "323671",
                   Name = "Internal - Empower Phase II"
               });

            modelBuilder.Entity<Activity>()
                .HasData(
               new Activity()
               {
                   Id = 1,
                   Number = "45789-US01",
                   Name = "Maintenance & Support"
               },
               new Activity()
               {
                   Id = 2,
                   Number = "45795-US01",
                   Name = "Design/Build/Test"
               },
               new Activity()
               {
                   Id = 3,
                   Number = "45796-US01",
                   Name = "Plan"
               },
               new Activity()
               {
                   Id = 4,
                   Number = "45797-US01",
                   Name = "Research"
               },
               new Activity()
               {
                   Id = 5,
                   Number = "45799-US01",
                   Name = "Project Management"
               });

            modelBuilder.Entity<Category>()
                .HasData(
               new Category()
               {
                   Id = 1,
                   Name = "Analyst"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Functional Lead"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Project Manager"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Scrum Master"
               },
               new Category()
               {
                   Id = 5,
                   Name = "Technical Architect"
               },
               new Category()
               {
                   Id = 6,
                   Name = "Technical Lead"
               },
               new Category()
               {
                   Id = 7,
                   Name = "Tester"
               });

            modelBuilder.Entity<LineProperty>()
                .HasData(
               new LineProperty()
               {
                   Id = 1,
                   Name = "Billable"
               },
               new LineProperty()
               {
                   Id = 2,
                   Name = "Subcontractor fees"
               },
               new LineProperty()
               {
                   Id = 3,
                   Name = "Extra hours"
               },
               new LineProperty()
               {
                   Id = 4,
                   Name = "Fixed price"
               },
               new LineProperty()
               {
                   Id = 5,
                   Name = "Internal"
               },
               new LineProperty()
               {
                   Id = 6,
                   Name = "Non billable"
               },
               new LineProperty()
               {
                   Id = 7,
                   Name = "Time-off"
               });

        }
    }
}