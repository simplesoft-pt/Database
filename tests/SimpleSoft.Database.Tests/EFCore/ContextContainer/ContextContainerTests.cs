using System;
using Microsoft.Extensions.Options;
using Xunit;

namespace SimpleSoft.Database.EFCore.ContextContainer
{
    public class ContextContainerTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _dbContextFixture;

        public ContextContainerTests(DbContextFixture dbContextFixture)
        {
            _dbContextFixture = dbContextFixture;
        }

        [Fact]
        public void Constructor_DbContext_Null_Throws_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new EFCoreContextContainer(
                null,
                Options.Create(new EFCoreContextContainerOptions())
            ));

            Assert.NotNull(ex);
            Assert.Equal("context", ex.ParamName);
        }

        [Fact]
        public void Constructor_Options_Null_Throws_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new EFCoreContextContainer(
                _dbContextFixture.Context,
                null
            ));

            Assert.NotNull(ex);
            Assert.Equal("options", ex.ParamName);
        }
    }
}
