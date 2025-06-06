using Contracts;
using Microsoft.Extensions.Configuration;
using Moq;
using Service;
using Microsoft.Extensions.Configuration.Json;
using Entities.Models;
using Entites.Exceptions.EmailException;
using Entites.Exceptions.UsersException;

namespace UserService.Tests.UnitTests
{
    public class EmailServiceTests
    {
        private readonly IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly EmailService _emailService;

        private User _user;

        public EmailServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _emailService = new EmailService(_repositoryManagerMock.Object, _configuration);

            _user = new User()
            {
                Id = "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d",
                UserName = "ivanov@example.com",
                NormalizedUserName = "IVANOV@EXAMPLE.COM",
                Email = "ivanov@example.com",
                NormalizedEmail = "IVANOV@EXAMPLE.COM",
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "+79161234567",
                PasswordHash = "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "60bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "5b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                EmailConfirmToken = "e7d2e0a4-7b7d-4c9b-b1d3-2eec5e8e2e7f"

            };
        }

        [Fact]
        public async Task ConfirmEmailByToken_WithValidToken_ConfirmsEmailSuccessfully()
        {
            // Arrange
            var email = _user.Email;
            var token = "e7d2e0a4-7b7d-4c9b-b1d3-2eec5e8e2e7f";
            var trackChanges = false;

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserByEmail(email!, trackChanges)).ReturnsAsync(_user);

            // Act
            await _emailService.ConfirmEmailByToken(email!, token, trackChanges);

            // Assert
            Assert.True(_user.EmailConfirmed);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ConfirmEmailByToken_WhenEmailAlreadyConfirmed_ThrowsException()
        {
            // Arrange
            var email = _user.Email;
            var token = _user.EmailConfirmToken;
            var trackChanges = false;

            _user.EmailConfirmed = true;

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserByEmail(email!, trackChanges)).ReturnsAsync(_user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmailAlreadyConfirmedException>(() => _emailService.ConfirmEmailByToken(email!, token!, trackChanges));

            Assert.Equal("Email is already confirmed.", exception.Message);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task ConfirmEmailByToken_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var token = "any-token";
            var trackChanges = false;

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserByEmail(email, trackChanges)).ReturnsAsync((User)null!); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundByEmailException>(() => _emailService.ConfirmEmailByToken(email, token, trackChanges));

            Assert.Contains($"The User with email: {email} doesn't exist in the database.", exception.Message); 
        }
    }
}
