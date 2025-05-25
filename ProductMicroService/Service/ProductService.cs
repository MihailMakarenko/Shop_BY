using AutoMapper;
using Contracts;
using Entities.Exceptions.ProductsException;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductForUser(Guid userId, ProductForCreationDto productForCreation, bool trackChanges)
        {
            var productEntity = _mapper.Map<Product>(productForCreation);

            _repositoryManager.Product.CreateProduct(userId, productEntity);
            await _repositoryManager.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return productToReturn;
        }

        public async Task DeleteProductForUser(Guid id, Guid userId, bool trackChanges)
        {
            var product = await GetProductAndCheckIfExists(id, userId, trackChanges);

            _repositoryManager.Product.DeleteProduct(product);
            await _repositoryManager.SaveAsync();
        }

        public Task<IEnumerable<ProductDto>> GetAllProducts(bool trackChagnes)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductByUserAsync(Guid id, Guid userId, bool trackChanges)
        {
            // add check if user exists
            var product = await GetProductAndCheckIfExists(id, userId, trackChanges);

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public Task<IEnumerable<ProductDto>> GetProductsByUserAsync(Guid userId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProductForUser(Guid id, Guid userId, ProductForUpdateDto productForUpdate, bool trackChagnes)
        {
            var productEntity = await GetProductAndCheckIfExists(id, userId, trackChagnes);

            _mapper.Map(productForUpdate, productEntity);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Product> GetProductAndCheckIfExists(Guid id, Guid userId, bool trackChanges)
        {
            var productEnity = await _repositoryManager.Product.GetProductForUserAsync(id, userId, trackChanges);

            if (productEnity is null)
                throw new ProductNotFoundException(id);

            return productEnity;
        }
    }
}
