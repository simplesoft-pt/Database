using System;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSoft.Database.EFCore
{
    public static class TestSingletons
    {
        public static readonly string ConnectionString;
        public static readonly IServiceProvider Provider;

        static TestSingletons()
        {
            ConnectionString = new SqliteConnectionStringBuilder
            {
                DataSource = "SimpleSoftDatabaseEFCore",
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Private
            }.ToString();

            Provider = new ServiceCollection()
                .AddDbContext<TestDbContext>()
                .AddDbContextOperations<TestDbContext>(o =>
                {
                    o.NoTracking = true;
                    o.AutoSaveChanges = true;
                })
                .BuildServiceProvider(true);
        }
    }
}
