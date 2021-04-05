using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xunit;

namespace SimpleSoft.Database.EFCore.ContextContainer
{
    public partial class ContextContainerTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _dbContextFixture;

        public ContextContainerTests(DbContextFixture dbContextFixture)
        {
            _dbContextFixture = dbContextFixture;
        }

        [Fact]
        public void Constructor_DbContext_Null_Throws_ArgumentNullException()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new EFCoreContextContainer(
                null,
                Options.Create(new EFCoreContextContainerOptions())
            ));

            // assert
            Assert.NotNull(ex);
            Assert.Equal("context", ex.ParamName);
        }

        [Fact]
        public void Constructor_Options_Null_Throws_ArgumentNullException()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new EFCoreContextContainer(
                _dbContextFixture.Context,
                null
            ));

            // assert
            Assert.NotNull(ex);
            Assert.Equal("options", ex.ParamName);
        }

        [Fact]
        public void Query_Result_NotNull()
        {
            // arrange
            var container = CreateContainer();

            // act
            var query = container.Query<IdLongEntity>();

            // assert
            Assert.NotNull(query);
        }

        private EFCoreContextContainer CreateContainer() => new EFCoreContextContainer(
            _dbContextFixture.Context,
            Options.Create(new EFCoreContextContainerOptions
            {
                NoTracking = true,
                AutoSaveChanges = true
            })
        );
    }
}
