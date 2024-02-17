
using EclipeWorks.Challenger.Api.Validation;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;
using FluentValidation.AspNetCore;
using System.Globalization;

namespace EclipeWorks.Challenger.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var environmentName = string.Empty;

            var configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                                 .Build();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            string connectionString = GetConnectionStringsDatabase(configuration, builder);

            builder.Services.AddSingleton<IUnitOfWork>(new UnitOfWork(connectionString));

            ConfigServicesDependencyInjection(builder);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssemblyContaining(typeof(FilterReportManagerModelRequestValidator));
                conf.RegisterValidatorsFromAssemblyContaining(typeof(TaskProjectValidator));
                conf.RegisterValidatorsFromAssemblyContaining(typeof(CommentValidator));
                conf.AutomaticValidationEnabled = false;
                conf.ValidatorOptions.LanguageManager.Culture = new CultureInfo("en-US");
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static string GetConnectionStringsDatabase(IConfigurationRoot configuration, WebApplicationBuilder builder)
        {
            var server = configuration.GetValue<string>("ConnectionStrings:DbServer");
            var port = configuration.GetValue<string>("ConnectionStrings:DbPort");  // Default SQL Server port
            var user = configuration.GetValue<string>("ConnectionStrings:DbUser"); // Warning do not use the SA account
            var password = configuration.GetValue<string>("ConnectionStrings:Password");
            var database = configuration.GetValue<string>("ConnectionStrings:Database");

            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Trust Server Certificate=True;";

            //if (builder.Environment.IsDevelopment())
            //{
            //    connectionString = $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
            //}

            return connectionString;
        }

        private static void ConfigServicesDependencyInjection(WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskProjectService, TaskProjectService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IReportManagerService, ReportManagerService>();
            builder.Services.AddScoped<IReportManagerValidatorService, ReportManagerValidatorService>();
            builder.Services.AddScoped<ITaskProjectValidatorService, TaskProjectValidatorService>();
            builder.Services.AddScoped<IOwnerService, OwnerService>();

        }
    }
}
