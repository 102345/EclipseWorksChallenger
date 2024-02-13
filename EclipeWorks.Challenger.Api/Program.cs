
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.InfraStructure.ConnectionDb;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

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

            builder.Services.AddSingleton<IReaderStringConnectionDb>(new ReaderStringConnectionDb(connectionString));

            ConfigServicesDependencyInjection(builder);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskProjectService, TaskProjectService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IReportManagerService, ReportManagerService>();

        }
    }
}
