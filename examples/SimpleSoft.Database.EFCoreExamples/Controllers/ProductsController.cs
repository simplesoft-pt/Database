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
        [
            Consumes("application/json"),
            Produces("application/json"),
            ProducesResponseType(200, Type = typeof(ProductModel)),
            ProducesResponseType(404, Type = typeof(ErrorModel))
        ]
        public async Task<IActionResult> GetById(
            [FromServices] IReadByExternalId<ProductEntity> productByExternalId,
            [FromRoute] Guid id,
            CancellationToken ct
        )
        {
            var product = await productByExternalId.ReadAsync(id, ct);
            if (product == null)
            {
                return NotFound(new ErrorModel
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
        [
            Consumes("application/json"),
            Produces("application/json"),
            ProducesResponseType(200, Type = typeof(ProductModel)),
            ProducesResponseType(409, Type = typeof(ErrorModel))
        ]
        public async Task<IActionResult> Create(
            [FromServices] IQueryable<ProductEntity> productQuery,
            [FromServices] ICreate<ProductEntity> productCreate,
            [FromBody] CreateProductModel model,
            CancellationToken ct
        )
        {
            if (await productQuery.AnyAsync(p => p.Code == model.Code, ct))
            {
                return Conflict(new ErrorModel
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

            return CreatedAtAction(nameof(GetById), new {id = product.ExternalId}, new ProductModel
            {
                Id = product.ExternalId,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            });
        }
    }
}