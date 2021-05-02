# Database
Small .NET library that helps to decouple database and business layers by exposing a wide range of interfaces that can be implemented using your favorite ORM, micro-ORM, ADO.NET or even all since everything is plugable and implementation can be swapped per entity.

## Installation
The library is available via [NuGet](https://www.nuget.org/packages?q=simplesoft.database) packages:

| NuGet | Description | Version |
| --- | --- | --- |
| [SimpleSoft.Database.Contracts](https://www.nuget.org/packages/simplesoft.database.contracts) | database contracts (IReadById, ICreate, IDelete, ...) | [![NuGet](https://img.shields.io/nuget/vpre/simplesoft.database.contracts.svg)](https://www.nuget.org/packages/simplesoft.database.contracts) |
| [SimpleSoft.Database.EFCore](https://www.nuget.org/packages/simplesoft.database.efcore) | Entity Framework Core implementation | [![NuGet](https://img.shields.io/nuget/vpre/simplesoft.database.efcore.svg)](https://www.nuget.org/packages/simplesoft.database.efcore) |

## Compatibility
This library is compatible with the following frameworks:

* `SimpleSoft.Mediator.Contracts`
  * .NET Framework 4.0+;
  * .NET Standard 1.0+;
* `SimpleSoft.Mediator`
  * .NET Standard 1.3+;

## Usage
Here is a simple example how to setup and use within an ASP.NET Core application:

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddDbContext<ExampleContext>(o => o.UseInMemoryDatabase("ExampleDatabase"))
            .AddDbContextOperations<ExampleContext>(); // this line here

        services.AddMvc();
    }
    
    // ...
}

public class ExampleContext : DbContext
{
    public ExampleContext(DbContextOptions<ExampleContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProductEntity>(cfg =>
        {
            cfg.MapPrimaryKey();
            cfg.MapExternalId();
            cfg.HasIndex(e => e.Code).IsUnique();

            cfg.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(8);
            cfg.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            cfg.Property(e => e.Price)
                .IsRequired();
        });
    }
}

[Route("products")]
public class ProductsController : Controller
{
    [HttpGet("")]
    public async Task<IEnumerable<ProductModel>> GetAllAsync(
        [FromServices] IQueryable<ProductEntity> productQuery,
        CancellationToken ct
    )
    {
        return await productQuery.Select(p => new ProductModel
        {
            Id = p.ExternalId,
            Code = p.Code,
            Name = p.Name,
            Price = p.Price
        }).ToListAsync(ct);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IReadByExternalId<ProductEntity> productByExternalId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var product = await productByExternalId.ReadAsync(id, ct);
        if (product == null)
        {
            return NotFound(new
            {
                Message = $"Product {id} not found"
            });
        }

        return Ok(new ProductModel
        {
            Id = product.ExternalId,
            Code = product.Code,
            Name = product.Name,
            Price = product.Price
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromServices] IQueryable<ProductEntity> productQuery,
        [FromServices] ICreate<ProductEntity> productCreate,
        [FromBody] CreateProductModel model,
        CancellationToken ct
    )
    {
        if (await productQuery.AnyAsync(p => p.Code == model.Code, ct))
        {
            return Conflict(new
            {
                Message = "Duplicated product code"
            });
        }

        var product = await productCreate.CreateAsync(new ProductEntity
        {
            ExternalId = Guid.NewGuid(),
            Code = model.Code,
            Name = model.Name,
            Price = model.Price
        }, ct);

        return CreatedAtAction(nameof(GetByIdAsync), new ProductModel
        {
            Id = product.ExternalId,
            Code = product.Code,
            Name = product.Name,
            Price = product.Price
        });
    }
}
```
