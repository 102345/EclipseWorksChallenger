
using EclipeWorks.Challenger.Api.Validation;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
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

            var environmentName = Environment.GetEnvironmentVariable("CONSOLENETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                                 .Build();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = configuration.GetValue<string>("ConnectionStrings:EclipseWorksChallengerDb");

            builder.Services.AddSingleton<IUnitOfWork>(new UnitOfWork(connectionString));

            ConfigServicesDependencyInjection(builder);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

        private static void ConfigServicesDependencyInjection(WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskProjectService, TaskProjectService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IReportManagerService, ReportManagerService>();
            builder.Services.AddScoped<IReportManagerValidatorService, ReportManagerValidatorService>();
            builder.Services.AddScoped<ITaskProjectValidatorService, TaskProjectValidatorService>();

        }
    }
}
