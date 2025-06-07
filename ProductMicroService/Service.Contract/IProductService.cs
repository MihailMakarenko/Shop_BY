using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;

namespace Service.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts( SieveModel sieveModel,bool trackChanges);
        Task<ProductDto> GetProductByUserAsync(Guid id, Guid userId, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductsByUserAsync(Guid userId, SieveModel sieveModel, bool trackChanges);
        Task<ProductDto> CreateProductForUser(Guid userId, ProductForCreationDto productForCreation, bool trackChanges);
        Task DeleteProductForUser(Guid id, Guid userId, bool trackChanges);
        Task UpdateProductForUser(Guid id, Guid userId, ProductForUpdateDto productForUpdate,bool trackChanges);
        Task DeactivateProductForUser(Guid userId, bool trackChanges);
    }
}
