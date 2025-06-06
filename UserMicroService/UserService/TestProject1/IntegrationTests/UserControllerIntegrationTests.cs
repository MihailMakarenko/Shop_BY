using FluentAssertions;
using Shared.DataTransferObjects.UserDto;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using UserService.Tests.IntegrationTests.Helpers;

namespace UserService.Tests.IntegrationTests
{
    [Collection("UserIntegration")]
    public class UserControllerIntegrationTests : IClassFixture<Factory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTests(Factory<Program> factory) => _client = factory.CreateClient();

        [Theory]
        [InlineData("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d")]
        public async Task GetUserById_WithValidIdAndAccess_ReturnsUser(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/users/{id}");
            request.Headers.Add("Authorization", "Bearer " + JwtGenerator.GenerateJwt("ivanov@example.com", id.ToString()).Result);

            var response = _client.SendAsync(request).Result;
            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var result = JsonSerializer.Deserialize<UserDto>(responseString, options);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result!.Id.Should().Be(id);

            SeedData.ResetData();
        }

        [Theory]
        [InlineData("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d")]
        public async Task DeleteUser_WithValidId_ReturnsNoContent(Guid id)
        {
            // Arrange
            var token = await JwtGenerator.GenerateJwt("ivanov@example.com", id.ToString());
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/users/{id}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            SeedData.ResetData();
        }

        [Theory]
        [InlineData("b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e")]
        public async Task UpdateUser_WithValidUser_ReturnsNoContent(Guid id)
        {
            var userUpdate = new UserForUpdateDto
            {
                FirstName = "NewName",
                LastName = "NewLastName",
                Email = "misha@gmail.com",
                PhoneNumber = "256788345",
                Password = "c213tRswtw!"
            };

            var token = await JwtGenerator.GenerateJwt("petrov@example.com", id.ToString());

            var updateRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/users/{id}")
            {
                Content = JsonContent.Create(userUpdate)
            };
            updateRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var updateResponse = await _client.SendAsync(updateRequest);

            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/users/{id}");
            getRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var getResponse = await _client.SendAsync(getRequest);

            getResponse.EnsureSuccessStatusCode();

            var getResponseString = await getResponse.Content.ReadAsStringAsync();
            var getResult = JsonSerializer.Deserialize<UserDto>(
                getResponseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            getResult.Should().NotBeNull();
            getResult!.FullName.Should().Be($"{userUpdate.FirstName} {userUpdate.LastName}");
            getResult.Email.Should().Be(userUpdate.Email);
            getResult.PhoneNumber.Should().Be(userUpdate.PhoneNumber);

            SeedData.ResetData();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task GetUserCollections_WithValidIdAndAccess_ReturnsUsers(IEnumerable<Guid> testData)
        {

            string ids = string.Join("&ids=", testData.Select(id => id.ToString()));
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/users/collection?ids={ids}");
            var token = await JwtGenerator.GenerateJwt("olegova@example.com", "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d", "Admin");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await _client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
            var getResult = JsonSerializer.Deserialize<List<UserDto>>(responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            response.EnsureSuccessStatusCode();
            getResult![0].Email.Should().Be("ivanov@example.com");
            getResult[1].Email.Should().Be("petrov@example.com");

            SeedData.ResetData();
        }

        public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
    new object[] { new List<Guid>
    {
        new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
        new Guid("b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e"),
    }},
        };
    }
}
