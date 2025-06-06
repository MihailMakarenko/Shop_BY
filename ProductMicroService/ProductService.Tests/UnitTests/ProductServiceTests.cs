using AutoMapper;
using Contracts;
using Entities.Exceptions.ProductsException;
using Entities.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;
using Sieve.Services;

namespace ProductService.Tests.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ISieveProcessor> _sieveProcessorMock;
        private readonly Service.ProductService _productService;

        private readonly Product _product;
        private readonly ProductDto _productDto;
        private readonly ProductForCreationDto _productForCreationDto;
        private readonly ProductForUpdateDto _productForUpdateDto;
        private readonly List<Product> _products;

        public ProductServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            var sieveOptionsMock = new Mock<IOptions<SieveOptions>>();
            _sieveProcessorMock = new Mock<ISieveProcessor>();
            _productService = new Service.ProductService(_repositoryManagerMock.Object, _mapperMock.Object, _sieveProcessorMock.Object);
            _product = new Product
            {
                Id = Guid.Parse("a1b2c3d4-1234-5678-9101-112131415161"),
                Name = "OLED TV 55",
                CreatedAt = new DateTime(2023, 1, 15, 14, 30, 0),
                UpdatedAt = new DateTime(2023, 6, 20, 10, 15, 0),
                CreatedByUserId = Guid.Parse("f1f2f3f4-1234-5678-9101-112131415161").ToString(),
                Description = "Ultra-thin TV with perfect black levels",
                Price = 1200.00m,
                IsAvailable = true
            };

            _productDto = new ProductDto
            {
                Id = _product.Id,
                Name = _product.Name,
                CreatedAt = _product.CreatedAt,
                UpdateAt = _product.UpdatedAt,
                CreatedByUserId = _product.CreatedByUserId,
                Description = _product.Description,
                Price = _product.Price,
                IsAvailable = _product.IsAvailable,
            };

            _productForCreationDto = new ProductForCreationDto
            {
                Name = "OLED TV 55",
                Description = "Ultra-thin TV with perfect black levels",
                Price = 1200.00m,
            };

            _productForUpdateDto = new ProductForUpdateDto
            {
                Name = "OLED TV 55 Update",
                Description = "Ultra-thin TV with perfect black levels Update",
                Price = 1201.00m,
            };

            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("a1b2c3d4-1234-5678-9101-112131415161"),
                    Name = "Test",
                    CreatedAt = new DateTime(2023, 1, 15, 14, 30, 0),
                    UpdatedAt = new DateTime(2023, 6, 20, 10, 15, 0),
                    CreatedByUserId = Guid.Parse("f1f2f3f4-1234-5678-9101-112131415161").ToString(),
                    Description = "Ultra-thin TV with perfect black levels",
                    Price = 1200.00m,
                    IsAvailable = true
                },
                new Product
                {
                    Id = Guid.Parse("b2c3d4e5-2345-6789-0123-456789abcdef"),
                    Name = "Smartphone X10",
                    CreatedAt = new DateTime(2023, 3, 10, 09, 45, 0),
                    UpdatedAt = new DateTime(2023, 5, 15, 16, 20, 0),
                    CreatedByUserId = Guid.Parse("e1e2e3e4-2345-6789-0123-456789abcdef").ToString(),
                    Description = "Flagship smartphone with 108MP camera",
                    Price = 999.99m,
                    IsAvailable = true
                },
                new Product
                {
                    Id = Guid.Parse("c3d4e5f6-3456-7890-1234-56789abcdef0"),
                    Name = "Test",
                    CreatedAt = new DateTime(2023, 2, 5, 11, 20, 0),
                    UpdatedAt = new DateTime(2023, 4, 18, 14, 10, 0),
                    CreatedByUserId = Guid.Parse("d1d2d3d4-3456-7890-1234-56789abcdef0").ToString(),
                    Description = "Noise-cancelling headphones with 30h battery",
                    Price = 349.95m,
                    IsAvailable = false
                },
                new Product
                {
                    Id = Guid.Parse("d4e5f6a7-4567-8901-2345-6789abcdef01"),
                    Name = "Gaming Laptop ZX",
                    CreatedAt = new DateTime(2023, 4, 1, 13, 15, 0),
                    UpdatedAt = new DateTime(2023, 7, 5, 09, 30, 0),
                    CreatedByUserId = Guid.Parse("c1c2c3c4-4567-8901-2345-6789abcdef01").ToString(),
                    Description = "High-performance laptop with RTX 4080",
                    Price = 2499.00m,
                    IsAvailable = true
                }
            };
        }

        [Fact]
        public async Task GetProductByUserAsync_WhenProductExists_ReturnsProductDto()
        {
            // Arrange
            var productId = _product.Id;
            var userId = Guid.Parse(_product.CreatedByUserId!);

            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, false)).ReturnsAsync(_product);

            _mapperMock.Setup(x => x.Map<ProductDto>(_product)).Returns(_productDto);

            // Act
            var result = await _productService.GetProductByUserAsync(productId, userId, false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(_productDto);

            _repositoryManagerMock.Verify(x => x.Product.GetProductForUserAsync(productId, userId, false), Times.Once);
            _mapperMock.Verify(x => x.Map<ProductDto>(_product), Times.Once);
        }

        [Fact]
        public async Task GetProductByUserAsync_ThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var productId = _product.Id;
            var userId = Guid.Parse(_product.CreatedByUserId!);

            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, false)).ReturnsAsync((Product?)null);

            // Act/Assert
            await _productService.Invoking(x => x.GetProductByUserAsync(productId, userId, false))
                .Should().ThrowAsync<ProductNotFoundException>();
        }

        [Fact]
        public async Task DeleteProductForUser_ShouldDelete_WhenProductExists()
        {
            // Arrange
            var productId = _product.Id;
            var userId = Guid.Parse(_product.CreatedByUserId!);

            _repositoryManagerMock.Setup(x => x.Product.DeleteProduct(_product)).Verifiable();
            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, false)).ReturnsAsync(_product).Verifiable();

            // Act
            await _productService.DeleteProductForUser(productId, userId, trackChanges: false);

            // Assert
            _repositoryManagerMock.Verify(x => x.Product.DeleteProduct(_product), Times.Once);
            _repositoryManagerMock.VerifyAll();
        }

        [Fact]
        public async Task CreateProductForUser_ShouldReturnCreatedModel_WhenProductIsValid()
        {
            // Arrange
            var userId = "f1f2f3f4-1234-5678-9101-112131415161";
            _mapperMock.Setup(m => m.Map<Product>(_productForCreationDto)).Returns(_product).Verifiable();
            _mapperMock.Setup(m => m.Map<ProductDto>(_product)).Returns(_productDto).Verifiable();
            _repositoryManagerMock.Setup(x => x.Product.CreateProduct(Guid.Parse(userId), _product)).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _productService.CreateProductForUser(Guid.Parse(userId), _productForCreationDto, trackChanges: false);

            // Assert
            result.Should().BeEquivalentTo(_productDto);
            _repositoryManagerMock.VerifyAll();
            _mapperMock.Verify(m => m.Map<Product>(_productForCreationDto), Times.Once);
        }

        [Fact]
        public async Task UpdateProductForUser_ShouldUpdateCorrectly_WhenIdAndModelProvided()
        {
            // Arrange
            var userId = Guid.Parse(_product.CreatedByUserId!);
            var productId = _product.Id;

            _mapperMock.Setup(m => m.Map(_productForUpdateDto, _product)).Verifiable();
            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, true)).ReturnsAsync(_product).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _productService.UpdateProductForUser(productId, userId, _productForUpdateDto, true);

            // Assert
            _repositoryManagerMock.VerifyAll();
            _mapperMock.VerifyAll();
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }


        [Fact]
        public async Task GetAllProducts_ShouldReturnFilteredAndMappedProducts()
        {
            // Arrange
            var sieveModel = new SieveModel { Filters = "Name==OLED TV 55" };
            var trackChanges = false;
            var testProduct = _product;
            var testProducts = new List<Product> { testProduct }.AsQueryable();
            var filteredProducts = new List<Product> { testProduct }.AsQueryable();
            var expectedDto = _productDto;

            _repositoryManagerMock.Setup(x => x.Product.GetAllProducts(trackChanges)).Returns(testProducts);
            _sieveProcessorMock.Setup(x => x.Apply(sieveModel,testProducts, null, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(filteredProducts);
            _mapperMock.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(expectedDto);

            // Act
            var result = await _productService.GetAllProducts(sieveModel, trackChanges);


            // Assert
            result.Should().NotBeEmpty();
            var resultList = result.ToList();
            resultList.Should().ContainSingle();
            resultList.First().Should().BeEquivalentTo(expectedDto);
          
            _repositoryManagerMock.Verify(x => x.Product.GetAllProducts(trackChanges), Times.Once);
            _sieveProcessorMock.Verify(x => x.Apply(sieveModel, testProducts, null, It.IsAny<bool>(),It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            _mapperMock.Verify(x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Once);
        }


        [Fact]
        public async Task GetAllProductsForUser_ShouldReturnFilteredAndMappedProducts()
        {
            // Arrange
            var userId = Guid.Parse("f1f2f3f4-1234-5678-9101-112131415161");
            var sieveModel = new SieveModel { Filters = "Name==OLED TV 55" };
            var trackChanges = false;
            var testProduct = _product;
            var testProducts = new List<Product> { testProduct }.AsQueryable();
            var filteredProducts = new List<Product> { testProduct }.AsQueryable();
            var expectedDto = _productDto;

            _repositoryManagerMock.Setup(x => x.Product.GetProductsForUserAsync(userId,trackChanges)).Returns(testProducts);
            _sieveProcessorMock.Setup(x => x.Apply(sieveModel, testProducts, null, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(filteredProducts);
            _mapperMock.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(expectedDto);

            // Act
            var result = await _productService.GetProductsByUserAsync(userId,sieveModel, trackChanges);


            // Assert
            result.Should().NotBeEmpty();
            var resultList = result.ToList();
            resultList.Should().ContainSingle();
            resultList.First().Should().BeEquivalentTo(expectedDto);

            _repositoryManagerMock.Verify(x => x.Product.GetProductsForUserAsync(userId, trackChanges), Times.Once);
            _sieveProcessorMock.Verify(x => x.Apply(sieveModel, testProducts, null, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            _mapperMock.Verify(x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductForUser_ShouldThrow_WhenProductNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, false)).ReturnsAsync((Product)null!);

            // Act & Assert
            await _productService.Invoking(x => x.DeleteProductForUser(productId, userId, false)).Should().ThrowAsync<ProductNotFoundException>();
        }

        [Fact]
        public async Task UpdateProductForUser_ShouldThrow_WhenProductNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var updateDto = new ProductForUpdateDto();

            _repositoryManagerMock.Setup(x => x.Product.GetProductForUserAsync(productId, userId, true)).ReturnsAsync((Product)null!);

            // Act & Assert
            await _productService.Invoking(x => x.UpdateProductForUser(productId, userId, updateDto, true)).Should().ThrowAsync<ProductNotFoundException>();
        }

    }
}