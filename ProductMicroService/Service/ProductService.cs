using AutoMapper;
using Contracts;
using Entities.Exceptions.ProductsException;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;
using Sieve.Services;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
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

        public async Task<IEnumerable<ProductDto>> GetAllProducts(SieveModel sieveModel, bool trackChanges)
        {
            var products = _repositoryManager.Product.GetAllProducts(trackChanges);

            var filteredQuery = _sieveProcessor.Apply(sieveModel, products);
            return await _mapper.ProjectTo<ProductDto>(filteredQuery).ToListAsync();
        }

        public async Task<ProductDto> GetProductByUserAsync(Guid id, Guid userId, bool trackChanges)
        {
            // add check if user exists
            var product = await GetProductAndCheckIfExists(id, userId, trackChanges);

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByUserAsync(Guid userId, SieveModel sieveModel, bool trackChanges)
        {
            var products =  _repositoryManager.Product.GetProductsForUserAsync(userId, trackChanges);
            
            var filteredQuery = _sieveProcessor.Apply(sieveModel, products);
            return await _mapper.ProjectTo<ProductDto>(filteredQuery).ToListAsync();
        }

        public async Task UpdateProductForUser(Guid id, Guid userId, ProductForUpdateDto productForUpdate, bool trackChanges)
        {
            var productEntity = await GetProductAndCheckIfExists(id, userId, trackChanges);

            _mapper.Map(productForUpdate, productEntity);
            productEntity.UpdatedAt = DateTime.Now;

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
