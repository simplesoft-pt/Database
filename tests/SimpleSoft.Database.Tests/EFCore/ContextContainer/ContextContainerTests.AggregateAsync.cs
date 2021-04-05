using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpleSoft.Database.EFCore.ContextContainer
{
    public partial class ContextContainerTests
    {
        [Fact]
        public async Task AggregateAsync_WithParameter_Aggregator_Null_Throws_ArgumentNullException()
        {
            // arrange
            var container = CreateContainer();

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await container.AggregateAsync<object, object>(
                    null,
                    new object(),
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
    }
}
