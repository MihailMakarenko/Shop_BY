using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Commands.ProductCommands.CreateProduct;
using Service.Commands.ProductCommands.DeleteProduct;
using Service.Commands.ProductCommands.UpdateProduct;
using Service.Contract;
using Service.Queries.ProductQueries.GetAllProducts;
using Service.Queries.ProductQueries.GetProductForUser;
using Service.Queries.ProductQueries.GetProductsForUser;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;
using System.Security.Claims;

namespace ProductService.Presentation.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/products")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {

        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductForUser(Guid id, Guid userId)
        {
            if (!await CanAccessUserAsync(User, userId))
                return Forbid();

            await _mediator.Send(new DeleteProductCommand(userId, id));
            
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductForUser(Guid userId, [FromBody] ProductForCreationDto productForCreation)
        {
            if (!await CanAccessUserAsync(User, userId))
                return Forbid();

            var result = await _mediator.Send(new CreateProductCommand(userId, productForCreation));

            return CreatedAtRoute("GetProductForUser", new { userId, id = result.Id }, result);
        }

        [HttpGet("{id:guid}", Name = "GetProductForUser")]
        public async Task<IActionResult> GetProductForUser(Guid id, Guid userId)
        {
            var product = await _mediator.Send(new GetProductForUserQuery(id, userId));

            return Ok(product);
        }

        [HttpGet(Name = "GetProductsForUser")]
        public async Task<IActionResult> GetProductsForUser(Guid userId, [FromQuery] SieveModel sieveModel)
        {
            var products = await _mediator.Send(new GetProductsForUserQuery(userId, sieveModel));
            
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("/api/products", Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] SieveModel sieveModel)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(sieveModel));

            return Ok(products);
        }

        [HttpPut("{id:guid}", Name = "UpdateProductForUser")]
        public async Task<IActionResult> UpdateProductForUser(Guid id, Guid userId, [FromBody] ProductForUpdateDto productForUpdate)
        {
            if (!await CanAccessUserAsync(User, userId))
                return Forbid();

            await _mediator.Send(new UpdateProductCommand(id, userId, productForUpdate));

            return NoContent();
        }

        private Task<bool> CanAccessUserAsync(ClaimsPrincipal user, Guid targetUserId)
        {
            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUser = user.IsInRole("User");

            if (targetUserId.ToString() != currentUserId)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}