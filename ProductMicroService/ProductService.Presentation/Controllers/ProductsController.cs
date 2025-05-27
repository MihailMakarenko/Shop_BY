using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;
using System.Security.Claims;

namespace ProductService.Presentation.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/products")]
    [ApiController]
    public class ProductsController(IServiceManager _serviceManager, IValidator<ProductForCreationDto> _createValidator, IValidator<ProductForUpdateDto> _updateValidator) : ControllerBase
    {
        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductForUser(Guid id, Guid userId)
        {
            if (!await CanAccessUserAsync(User, userId))
                return Forbid();

            await _serviceManager.ProductService.DeleteProductForUser(id, userId, trackChanges: false);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductForUser(Guid userId, [FromBody] ProductForCreationDto productForCreation)
        {
            if (!await CanAccessUserAsync(User, userId))
                return Forbid();

            _createValidator.ValidateAndThrow(productForCreation);

            var productToReturn = await _serviceManager.ProductService.CreateProductForUser(userId, productForCreation, trackChanges: false);

            return CreatedAtRoute("GetProductForUser", new { userId, id = productToReturn.Id }, productToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetProductForUser")]
        public async Task<IActionResult> GetProductForUser(Guid id, Guid userId)
        {
            var product = await _serviceManager.ProductService.GetProductByUserAsync(id, userId, trackChanges: false);

            return Ok(product);
        }

        [HttpGet(Name = "GetProductsForUser")]
        public async Task<IActionResult> GetProductsForUser(Guid userId, [FromQuery] SieveModel sieveModel)
        {
            var products = await _serviceManager.ProductService.GetProductsByUserAsync(userId, sieveModel, trackChanges: false);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("/api/products", Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] SieveModel sieveModel)
        {
            var products = await _serviceManager.ProductService.GetAllProducts(sieveModel, trackChanges: false);

            return Ok(products);
        }

        [HttpPut("{id:guid}", Name = "UpdateProductForUser")]
        public async Task<IActionResult> UpdateProductForUser(Guid id, Guid userId, [FromBody] ProductForUpdateDto productForUpdate)
        {
            if(!await CanAccessUserAsync(User, userId))
                return Forbid();
            
            _updateValidator.ValidateAndThrow(productForUpdate);

            await _serviceManager.ProductService.UpdateProductForUser(id, userId, productForUpdate, trackChanges: true);

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