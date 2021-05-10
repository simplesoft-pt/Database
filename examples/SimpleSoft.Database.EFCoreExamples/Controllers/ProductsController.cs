using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Database.EFCoreExamples.Entities;

namespace SimpleSoft.Database.EFCoreExamples.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        [
            Consumes("application/json"),
            Produces("application/json"),
            ProducesResponseType(200)
        ]
        public async Task<IEnumerable<ProductModel>> GetAll(
            [FromServices] IQueryable<ProductEntity> productQuery,
            [FromServices] IQueryable<PriceHistoryEntity> priceHistoryQuery,
            CancellationToken ct
        )
        {
            return await productQuery.Select(p => new ProductModel
            {
                Id = p.ExternalId,
                Code = p.Code,
                Name = p.Name,
                Price = priceHistoryQuery
                    .Where(ph => ph.ProductId == p.Id)
                    .OrderByDescending(ph => ph.CreatedOn)
                    .First()
                    .Value
            }).ToListAsync(ct);
        }

        [HttpGet("{id:guid}")]
        [
            Consumes("application/json"),
            Produces("application/json"),
            ProducesResponseType(200, Type = typeof(ProductModel)),
            ProducesResponseType(404, Type = typeof(ErrorModel))
        ]
        public async Task<IActionResult> GetById(
            [FromServices] IReadByExternalId<ProductEntity> productByExternalId,
            [FromServices] IQueryable<PriceHistoryEntity> priceHistoryQuery,
            [FromRoute] Guid id,
            CancellationToken ct
        )
        {
            var product = await productByExternalId.ReadAsync(id, ct);
            if (product == null)
            {
                return NotFound(new ErrorModel
                {
                    Message = $"Product '{id}' not found"
                });
            }

            var price = await priceHistoryQuery
                .Where(ph => ph.ProductId == product.Id)
                .OrderByDescending(ph => ph.CreatedOn)
                .FirstAsync(ct);

            return Ok(new ProductModel
            {
                Id = product.ExternalId,
                Code = product.Code,
                Name = product.Name,
                Price = price.Value
            });
        }

        [HttpPost]
        [
            Consumes("application/json"),
            Produces("application/json"),
            ProducesResponseType(200, Type = typeof(ProductModel)),
            ProducesResponseType(409, Type = typeof(ErrorModel))
        ]
        public async Task<IActionResult> Create(
            [FromServices] ITransaction transaction,
            [FromServices] IQueryable<ProductEntity> productQuery,
            [FromServices] ICreate<ProductEntity> productCreate,
            [FromServices] ICreate<PriceHistoryEntity> priceHistoryCreate,
            [FromBody] CreateProductModel model,
            CancellationToken ct
        )
        {
            if (await productQuery.AnyAsync(p => p.Code == model.Code, ct))
            {
                return Conflict(new ErrorModel
                {
                    Message = $"Product code '{model.Code}' already exists"
                });
            }

            await transaction.BeginAsync(ct);

            var product = await productCreate.CreateAsync(new ProductEntity
            {
                ExternalId = Guid.NewGuid(),
                Code = model.Code,
                Name = model.Name
            }, ct);

            var price = await priceHistoryCreate.CreateAsync(new PriceHistoryEntity
            {
                ProductId = product.Id,
                Value = model.Price,
                CreatedOn = DateTimeOffset.UtcNow
            }, ct);

            await transaction.CommitAsync(ct);

            return CreatedAtAction(nameof(GetById), new {id = product.ExternalId}, new ProductModel
            {
                Id = product.ExternalId,
                Code = product.Code,
                Name = product.Name,
                Price = price.Value
            });
        }
    }
}