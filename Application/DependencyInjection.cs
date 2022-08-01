using Application.Common.Interface;
using Application.Main;
using Application.Timesheets;
using Domain.Main;
using Domain.Timesheets;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEntityService<Activity>, ActivityService>();
            services.AddScoped<IEntityService<ApprovalStatus>, ApprovalStatusService>();
            services.AddScoped<IEntityService<Category>, CategoryService>();
            services.AddScoped<IEntityService<Customer>, CustomerService>();
            services.AddScoped<IEntityService<Employee>, EmployeeService>();
            services.AddScoped<IEntityService<Employee_Reviewer>, Employee_ReviewerService>();
            services.AddScoped<IEntityService<EmployeeType>, EmployeeTypeService>();
            services.AddScoped<IEntityService<LegalEntity>, LegalEntityService>();
            services.AddScoped<IEntityService<LineProperty>, LinePropertyService>();
            services.AddScoped<IEntityService<Location>, LocationService>();
            services.AddScoped<IEntityService<Project>, ProjectService>();
            services.AddScoped<IEntityService<Timesheet>, TimesheetService>();
            services.AddScoped<ITimesheetService, TimesheetService>();
            return services;
        }
    }
}