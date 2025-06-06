using FluentAssertions;
using ProductService.Tests.IntegrationTests.Helpers;
using Shared.DataTransferObjects.ProductDto;
using System.Net;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ProductService.Tests.IntegrationTests
{
    [Collection("ProductIntegration")]
    public class ProductControllerIntegrationTests : IClassFixture<Factory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerIntegrationTests(Factory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly string _userId = "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d";
        private readonly string _email = "ivanov@example.com";


        [Fact]
        public async Task CreateProduct_ValidData_ShouldReturnProductDto()
        {
            var _token = await JwtGenerator.GenerateJwt(_email, _userId);
            var productForCreation = new ProductForCreationDto()
            {
                Name = "OLED TV 55",
                Description = "Ultra-thin TV with perfect black levels",
                Price = 1200.00m,
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/users/{_userId}/products");
            request.Content = JsonContent.Create(productForCreation);
            request.Headers.Add("Authorization", "Bearer " + _token);

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            SeedData.ResetData();
        }

        [Fact]
        public async Task DeleteProudct_ValidProductId_ShouldReturnNoContent()
        {
            var _token = await JwtGenerator.GenerateJwt(_email, _userId);
            var productId = "A8A8A8A8-1111-1111-1111-111111111111";

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/users/{_userId}/products/{productId}");
            request.Headers.Add("Authorization", "Bearer " + _token);

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            SeedData.ResetData();
        }

        [Fact]
        public async Task GetProduct_ValidData_ShouldReturnProduct()
        {
            var _token = await JwtGenerator.GenerateJwt(_email, _userId);
            var productId = "A8A8A8A8-1111-1111-1111-111111111111";

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{_userId}/products/{productId}");
            request.Headers.Add("Authorization", "Bearer " + _token);

            var response = await _client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var result = JsonSerializer.Deserialize<ProductDto>(responseString, options);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result!.Name.Should().Be("Смартфон Samsung Galaxy S23");

            SeedData.ResetData();
        }

        [Fact]
        public async Task UpdateProduct_ValidData_ShouldReturnNoKontent()
        {
            var token = await JwtGenerator.GenerateJwt(_email, _userId);
            var productId = "A8A8A8A8-1111-1111-1111-111111111111";

            var productForUpdate = new ProductForUpdateDto()
            {
                Name = "OLEDTV55",
                Description = "Ultra-thinTVwithperfectblacklevels",
                Price = 1110.00m,
            };

            var request = new HttpRequestMessage(HttpMethod.Put, $"api/users/{_userId}/products/{productId}");
            request.Content = JsonContent.Create(productForUpdate);
            request.Headers.Add("Authorization", "Bearer " + token);

            var requestGetProduct = new HttpRequestMessage(HttpMethod.Get, $"api/users/{_userId}/products/{productId}");
            requestGetProduct.Headers.Add("Authorization", "Bearer " + token);

            var response = await _client.SendAsync(request);
            var responseGetProduct = await _client.SendAsync(requestGetProduct);

            response.EnsureSuccessStatusCode();

            var responseString = await responseGetProduct.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var result = JsonSerializer.Deserialize<ProductDto>(responseString, options);

            result.Should().NotBeNull();
            result!.Name.Should().Be("OLEDTV55");
            result.Description.Should().Be("Ultra-thinTVwithperfectblacklevels");
            result.Price.Should().Be(1110.00m);

            SeedData.ResetData();
        }

        [Fact]
        public async Task GetAllProductWithPagination_ValidData_ShouldReturnProduct()
        {
            var token = await JwtGenerator.GenerateJwt(_email, _userId, "Admin");

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/products?Page=2&PageSize=1");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var result = JsonSerializer.Deserialize<List<ProductDto>>(responseString, options);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("Смартфон Samsung Galaxy S23");

            SeedData.ResetData();
        }

        [Fact]
        public async Task GetAllProductsForUserWithPagination_ValidData_ShouldReturnProduct()
        {
            var token = await JwtGenerator.GenerateJwt(_email, _userId, "Admin");

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{_userId}/products?Page=2&PageSize=1");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var result = JsonSerializer.Deserialize<List<ProductDto>>(responseString, options);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("PlayStation 5");

            SeedData.ResetData();
        }

    }
}
