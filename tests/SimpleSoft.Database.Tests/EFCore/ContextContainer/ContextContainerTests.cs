using System;
using System.Threading;
using System.Threading.Tasks;
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

        [Fact]
        public async Task AggregateAsync_WithParameter_Aggregator_Null_Throws_ArgumentNullException()
        {
            // arrange
            var container = CreateContainer();
            var aggParam = new object();

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await container.AggregateAsync<object, object>(
                    null,
                    aggParam,
                    CancellationToken.None
                )
            );

            // assert
            Assert.NotNull(ex);
            Assert.Equal("aggregator", ex.ParamName);
        }

        [Fact]
        public async Task AggregateAsync_WithoutParameter_Aggregator_Null_Throws_ArgumentNullException()
        {
            // arrange
            var container = CreateContainer();

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await container.AggregateAsync<object>(
                    null,
                    CancellationToken.None
                )
            );

            // assert
            Assert.NotNull(ex);
            Assert.Equal("aggregator", ex.ParamName);
        }

        [Fact]
        public async Task AggregateAsync_WithParameter_SameInstance()
        {
            // arrange
            var container = CreateContainer();
            var aggParam = new object();

            // act
            var result = await container.AggregateAsync((context, param, ct) => Task.FromResult(new
            {
                Context = context,
                Param = param
            }), aggParam, CancellationToken.None);

            // assert
            Assert.NotNull(result);

            Assert.NotNull(result.Context);
            Assert.Same(_dbContextFixture.Context, result.Context);
            
            Assert.NotNull(result.Param);
            Assert.Same(aggParam, result.Param);
        }

        [Fact]
        public async Task AggregateAsync_WithoutParameter_SameInstance()
        {
            // arrange
            var container = CreateContainer();

            // act
            var result = await container.AggregateAsync((context, ct) => Task.FromResult(new
            {
                Context = context
            }), CancellationToken.None);

            // assert
            Assert.NotNull(result);
            
            Assert.NotNull(result.Context);
            Assert.Same(_dbContextFixture.Context, result.Context);
        }

        private EFCoreContextContainer CreateContainer() => new EFCoreContextContainer(
            _dbContextFixture.Context,
            Options.Create(new EFCoreContextContainerOptions())
        );
    }
}
