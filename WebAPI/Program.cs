using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Serilog;
using WebAPI.Controllers;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/HiSolTimesheet.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            //.WithOrigins("https://localhost:7129",
                            //                "http://localhost:5129")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = false;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters()
.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSingleton(typeof(IEntityControllerBase<,,,,>), typeof(EntityControllerBase<,,,,>));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
