using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects.UserDto;
using System.Net;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using UserService.Tests.IntegrationTests.Helpers;

namespace UserService.Tests.IntegrationTests
{
    [Collection("UserIntegration")]
    public class AuthControllerIntegrationTests : IClassFixture<Factory<Program>>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(Factory<Program> factory) => _client = factory.CreateClient();

        [Fact]
        public async Task RegisterUser_ValidData_ReturnLinks()
        {
            var userForCreation = new UserForCreationDto()
            {
                FirstName = "NewName",
                LastName = "NewLastName",
                Email = "12mischa5astneipts@gmail.com",
                PhoneNumber = "256788385",
                Password = "C1324BsT54@"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/authentication");
            request.Content = JsonContent.Create(userForCreation);

            var response = await _client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync(); 

            responseString.Should().Be("Registration was successful. Check email.");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            SeedData.ResetData();

        }

        [Fact]
        public async Task LoginUser_ValidData_ReturnToken()
        {
            var userForLogin = new UserForAuthenticationDto()
            {
                Email = "ivanov@example.com",
                Password = "Password1!"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/authentication/login");
            request.Content = JsonContent.Create(userForLogin);

            var response = await _client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
         

            var responseDto = JsonSerializer.Deserialize<TokenDto>(
            responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseDto!.AccessToken.Should().NotBeNull();
            responseDto.RefreshToken.Should().NotBeNull();

            SeedData.ResetData();
        }
   
    
        [Fact]
        public async Task ForgotPassword_ValidData_ReturnMessage() 
        {
            var email = "ivanov@example.com";

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/authentication/forgotpassword?email={email}");

            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Should().Be($"Check {email} email. You may now reset your password within 1 hour.");

            SeedData.ResetData();
        }

        [Fact]
        public async Task ConfirmEmail_ValidData_ReturnMessage()
        {
            var email = "ivanov@example.com";
            var emailToken = "11111111-2222-3333-4444-555555555555";

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/authentication/confirmemail?email={email}&token={emailToken}");
            
            var response = await _client.SendAsync(request);

            var responseSting = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseSting.Should().Be("Confirmation was successful.");
           
        }

    }
}
