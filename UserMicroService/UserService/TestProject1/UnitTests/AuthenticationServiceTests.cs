using AutoMapper;
using Contracts;
using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Shared.DataTransferObjects.UserDto;

namespace UserService.Tests.UnitTests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IOptions<JwtConfiguration>> _jwtConfigMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Service.AuthenticationService _authService;

        private readonly JwtConfiguration _jwtConfiguration;
        private readonly User _user;
        private readonly UserForCreationDto _userForRegistrationDto;
        private readonly UserForAuthenticationDto _userForAuthDto;

        public AuthenticationServiceTests()
        {
            _jwtConfiguration = new JwtConfiguration
            {
                SecretKey = "super-secret-test-key-1234567890abcdefghijklmnopqrstuvwxyz",
                ValidIssuer = "test-issuer.example.com",
                ValidAudience = "test-audience.example.com",
                ExpiresInMinutes = 30,
                ClockSkew = 5
            };

            _user = new User
            {
                Id = "test-user-id",
                UserName = "test@example.com",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "+1234567890"
            };

            _userForRegistrationDto = new UserForCreationDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                PhoneNumber = "+1234567890",
                Password = "TestPassword123!",
                Roles = new List<string> { "User" }
            };

            _userForAuthDto = new UserForAuthenticationDto
            {
                Email = "test@example.com",
                Password = "TestPassword123!"
            };


            _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null!, null!, null!, null!);

            _mapperMock = new Mock<IMapper>();
            _repositoryManagerMock = new Mock<IRepositoryManager>();

            _jwtConfigMock = new Mock<IOptions<JwtConfiguration>>();
            _jwtConfigMock.Setup(x => x.Value).Returns(_jwtConfiguration);

            _authService = new Service.AuthenticationService(
               _userManagerMock!.Object,
               _mapperMock!.Object,
               _jwtConfigMock!.Object,
               _roleManagerMock!.Object,
               _repositoryManagerMock!.Object);
        }

        [Fact]
        public async Task RegisterUser_WhenValidData_CreatesUserAndReturnsSuccess()
        {
            // Arrange
            _mapperMock.Setup(x => x.Map<User>(_userForRegistrationDto)).Returns(_user);
            _userManagerMock.Setup(x => x.CreateAsync(_user, _userForRegistrationDto.Password!)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(_user, "User")).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterUser(_userForRegistrationDto, _userForRegistrationDto.Email!);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task LoginUser_WithValidCredentials_ReturnsResult()
        {
            // Arrange
            var email = _userForAuthDto.Email;
            var password = _userForAuthDto.Password;

            _userManagerMock.Setup(x => x.FindByEmailAsync(email!)).ReturnsAsync(_user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(_user, password!)).ReturnsAsync(true);

            // Act
            var result = await _authService.LoginUser(_userForAuthDto);

            // Assert
            result.Should().BeTrue();

            _userManagerMock.Verify(x => x.FindByEmailAsync(email!), Times.Once);
            _userManagerMock.Verify(x => x.CheckPasswordAsync(_user, password!), Times.Once);
        }

        [Fact]
        public async Task ResetPassword_WithValidData_ResetsPasswordSuccessfully()
        {
            // Arrange
            var resetDto = new ResetPasswordDto
            {
                Email = "user@example.com",
                ResetToken = "valid-reset-token",
                NewPassword = "NewSecurePassword123!"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(resetDto.Email)).ReturnsAsync(_user);

            _userManagerMock.Setup(x => x.ResetPasswordAsync(_user, resetDto.ResetToken, resetDto.NewPassword)).ReturnsAsync(IdentityResult.Success);

            // Act
            await _authService.ResetPassword(resetDto);

            // Assert
            _userManagerMock.Verify(x => x.FindByEmailAsync(resetDto.Email), Times.Once);
            _userManagerMock.Verify(x => x.ResetPasswordAsync(_user, resetDto.ResetToken, resetDto.NewPassword), Times.Once);
        }

        [Fact]
        public async Task ForgotPassword_WithValidEmail_ReturnsResetToken()
        {
            // Arrange
            var email = _user.Email;
            var expectedToken = "generated-reset-token";

            _userManagerMock.Setup(x => x.FindByEmailAsync(email!)).ReturnsAsync(_user);

            _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(_user)).ReturnsAsync(expectedToken);

            _userManagerMock.Setup(x => x.UpdateAsync(_user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ForgotPassword(email!);

            // Assert
            result.Should().Be(expectedToken);

            _userManagerMock.Verify(x => x.FindByEmailAsync(email!), Times.Once);
            _userManagerMock.Verify(x => x.GeneratePasswordResetTokenAsync(_user), Times.Once);
            _userManagerMock.Verify(x => x.UpdateAsync(_user), Times.Once);
        }

    }
}
