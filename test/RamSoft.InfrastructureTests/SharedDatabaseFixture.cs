using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Profiles;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Infrastructure.Repositories.Base;
using SharedDatabaseSetup;
using System.Data.Common;

namespace RamSoft.InfrastructureTests
{
    public class SharedDatabaseFixture : IDisposable
    {

        private static IConfigurationProvider _configuration;
        public static IConfigurationProvider configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<MappingProfile>();
                    });
                }
                return _configuration;
            }
        }
        private static IMapper _mapper;
        public static IMapper mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = configuration.CreateMapper();
                }
                return _mapper;
            }

        }

        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        private string dbNameSQLDbContext = "IntegrationTestsDatabase.db";

        public SharedDatabaseFixture()
        {
            ConnectionSQLDbContext = new SqliteConnection($"Filename={dbNameSQLDbContext}");

            SeedSDADbContext();

            ConnectionSQLDbContext.Open();
        }

        public DbConnection ConnectionSQLDbContext { get; }

        public JiraDbContext CreateSQLContext(DbTransaction? transaction = null)
        {
            var context = new JiraDbContext(new DbContextOptionsBuilder<JiraDbContext>().UseSqlite(ConnectionSQLDbContext).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }
        //public UnitOfWork CreateUnitOfWork(JiraDbContext jiraDbContext, ICurrentUserService currentUserService)
        //{
        //    return new UnitOfWork(jiraDbContext, currentUserService, 
        //        new statesrepo
        //        );
        //}

        private void SeedSDADbContext()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateSQLContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        DatabaseSetup.SeedDataCustomDbContext(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose()
        {
            ConnectionSQLDbContext.Dispose();
        }
    }
}