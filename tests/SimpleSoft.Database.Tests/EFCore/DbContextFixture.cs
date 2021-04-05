using System;

namespace SimpleSoft.Database.EFCore
{
    public class DbContextFixture : IDisposable
    {
        public DbContextFixture()
        {
            var context = new TestDbContext();

            try
            {
                context.Database.EnsureCreated();
            }
            catch
            {
                context.Dispose();
                throw;
            }

            Context = context;
        }

        public TestDbContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
