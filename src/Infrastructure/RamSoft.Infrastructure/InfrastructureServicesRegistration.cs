using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RamSoft.Application.Contracts.Base;
using RamSoft.Infrastructure.Repositories.Base;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Infrastructure.Repositories.Jira;

namespace RamSoft.Infrastructure
{

    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JiraDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("SQLConnectionString")),
               ServiceLifetime.Scoped
               );


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IStatesRepository, StatesRepository>();
            services.AddScoped<ITaskBoardRepository, TaskBoardRepository>();
            services.AddScoped<ITaskBoardStatesRepository, TaskBoardStatesRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();

            return services;
        }

    }
    public class SDALeitnerBoxDbContextFactor : IDesignTimeDbContextFactory<JiraDbContext>
    {
        public JiraDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<JiraDbContext>();
            var connectionString = configuration.GetConnectionString("SQLConnectionString");
            builder.UseSqlServer(connectionString);
            return new JiraDbContext(builder.Options);

        }
    }
}
