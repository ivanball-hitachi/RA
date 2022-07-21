using Application.Common.Interface;
using Domain.Main.DTO;
using Domain.Timesheets.DTO;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configuration.Main;
using Infrastructure.Persistence.Configuration.Timesheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlite(configuration.GetConnectionString("HiSolTimesheetDBConnectionString"),
               //.EnableSensitiveDataLogging()
               //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>()!);
            services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<ActivityDTO>, ActivityDTOValidator>();
            services.AddScoped<IValidator<ActivityForCreationDTO>, ActivityDTOValidator>();
            services.AddScoped<IValidator<ActivityForUpdateDTO>, ActivityDTOValidator>();

            services.AddScoped<IValidator<ApprovalStatusDTO>, ApprovalStatusDTOValidator>();
            services.AddScoped<IValidator<ApprovalStatusForCreationDTO>, ApprovalStatusDTOValidator>();
            services.AddScoped<IValidator<ApprovalStatusForUpdateDTO>, ApprovalStatusDTOValidator>();

            services.AddScoped<IValidator<CategoryDTO>, CategoryDTOValidator>();
            services.AddScoped<IValidator<CategoryForCreationDTO>, CategoryDTOValidator>();
            services.AddScoped<IValidator<CategoryForUpdateDTO>, CategoryDTOValidator>();

            services.AddScoped<IValidator<CustomerDTO>, CustomerDTOValidator>();
            services.AddScoped<IValidator<CustomerForCreationDTO>, CustomerDTOValidator>();
            services.AddScoped<IValidator<CustomerForUpdateDTO>, CustomerDTOValidator>();

            services.AddScoped<IValidator<EmployeeDTO>, EmployeeDTOValidator>();
            services.AddScoped<IValidator<EmployeeForCreationDTO>, EmployeeDTOValidator>();
            services.AddScoped<IValidator<EmployeeForUpdateDTO>, EmployeeDTOValidator>();

            services.AddScoped<IValidator<EmployeeTypeDTO>, EmployeeTypeDTOValidator>();
            services.AddScoped<IValidator<EmployeeTypeForCreationDTO>, EmployeeTypeDTOValidator>();
            services.AddScoped<IValidator<EmployeeTypeForUpdateDTO>, EmployeeTypeDTOValidator>();

            services.AddScoped<IValidator<LegalEntityDTO>, LegalEntityDTOValidator>();
            services.AddScoped<IValidator<LegalEntityForCreationDTO>, LegalEntityDTOValidator>();
            services.AddScoped<IValidator<LegalEntityForUpdateDTO>, LegalEntityDTOValidator>();

            services.AddScoped<IValidator<LinePropertyDTO>, LinePropertyDTOValidator>();
            services.AddScoped<IValidator<LinePropertyForCreationDTO>, LinePropertyDTOValidator>();
            services.AddScoped<IValidator<LinePropertyForUpdateDTO>, LinePropertyDTOValidator>();

            services.AddScoped<IValidator<LocationDTO>, LocationDTOValidator>();
            services.AddScoped<IValidator<LocationForCreationDTO>, LocationDTOValidator>();
            services.AddScoped<IValidator<LocationForUpdateDTO>, LocationDTOValidator>();

            services.AddScoped<IValidator<ProjectDTO>, ProjectDTOValidator>();
            services.AddScoped<IValidator<ProjectForCreationDTO>, ProjectDTOValidator>();
            services.AddScoped<IValidator<ProjectForUpdateDTO>, ProjectDTOValidator>();

            services.AddScoped<IValidator<TimesheetDTO>, TimesheetDTOValidator>();
            services.AddScoped<IValidator<TimesheetForCreationDTO>, TimesheetDTOValidator>();
            services.AddScoped<IValidator<TimesheetForUpdateDTO>, TimesheetDTOValidator>();

            services.AddScoped<IValidator<TimesheetLineDTO>, TimesheetLineDTOValidator>();
            services.AddScoped<IValidator<TimesheetLineForCreationDTO>, TimesheetLineDTOValidator>();
            services.AddScoped<IValidator<TimesheetLineForUpdateDTO>, TimesheetLineDTOValidator>();

            services.AddScoped<IValidator<TimesheetLineDetailDTO>, TimesheetLineDetailDTOValidator>();
            services.AddScoped<IValidator<TimesheetLineDetailForCreationDTO>, TimesheetLineDetailDTOValidator>();
            services.AddScoped<IValidator<TimesheetLineDetailForUpdateDTO>, TimesheetLineDetailDTOValidator>();

            return services;
        }
    }
}