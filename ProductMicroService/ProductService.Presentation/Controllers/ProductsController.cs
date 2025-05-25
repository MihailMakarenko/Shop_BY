using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace ProductService.Presentation.Controllers
{
    [Route("api/users/{userId}/products")]
    [ApiController]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductForUser(Guid id, Guid userId)
        {
            await _serviceManager.ProductService.DeleteProductForUser(userId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductForUser(Guid userId, [FromBody] ProductForCreationDto productForCreation)
        {
            var productToReturn = await _serviceManager.ProductService.CreateProductForUser(userId, productForCreation, trackChanges: false);

            return CreatedAtRoute("GetProductForUser", new { userId, id = productToReturn.Id }, productToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetProductForUser")]
        public async Task<IActionResult> GetProductForUser(Guid id, Guid userId)
        {
            var product = await _serviceManager.ProductService.GetProductByUserAsync(id, userId, trackChanges: false);
            return Ok(product);
        }
    }
}
