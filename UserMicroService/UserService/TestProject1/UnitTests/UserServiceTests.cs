using AutoMapper;
using Contracts;
using Entites.Exceptions;
using Entities.Exceptions.UsersException;
using Entities.Models;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.UserDto;

namespace UserService.Tests.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Service.UserService _userService;

        private readonly User _user;
        private readonly UserDto _userDto;
        private readonly UserForUpdateDto _userForUpdate;
        private readonly List<User> _users;
        private readonly List<UserDto> _usersDto;

        public UserServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _userService = new Service.UserService(_repositoryManagerMock.Object, _mapperMock.Object);

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
                TwoFactorEnabled = false
            };

            _userDto = new UserDto()
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                Email = "ivanov@example.com",
                PhoneNumber = "+79161234567",
                FullName = "Иван Иванов"
            };

            _users = new List<User>()
            {
                new User
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
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e",
                UserName = "petrov@example.com",
                NormalizedUserName = "PETROV@EXAMPLE.COM",
                Email = "petrov@example.com",
                NormalizedEmail = "PETROV@EXAMPLE.COM",
                FirstName = "Петр",
                LastName = "Петров",
                PhoneNumber = "+79162345678",
                PasswordHash = "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "70bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "6b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f",
                UserName = "sergeev@example.com",
                NormalizedUserName = "SERGEEV@EXAMPLE.COM",
                Email = "sergeev@example.com",
                NormalizedEmail = "SERGEEV@EXAMPLE.COM",
                FirstName = "Сергей",
                LastName = "Сергеев",
                PhoneNumber = "+79163456789",
                PasswordHash = "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "80bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "7b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },

            };
            _usersDto = new List<UserDto>()
            {
                new UserDto
                {

                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                Email = "ivanov@example.com",
                FullName = "Иван Иванов",
                PhoneNumber = "+79161234567",
                },
                new UserDto
                {

                Id = Guid.Parse("b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e"),
                Email = "petrov@example.com",
                FullName = "Петр Петров",
                PhoneNumber = "+79162345678",
                },
                new UserDto
                {

                Id = Guid.Parse("c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f"),
                Email = "sergeev@example.com",
                FullName = "Сергей Сергеев",
                PhoneNumber = "+79163456789",
                },

            };

            _userForUpdate = new UserForUpdateDto()
            {
                FirstName = "NewName",
                LastName = "NewLastName",
                Email = "misha@gmail.com",
                PhoneNumber = "+375256788345",
                Password = "C1324BT54@"
            };
        }

        [Fact]
        public async Task GetUserById_WhenUserExists_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.Parse(_user.Id);
            var trackChanges = false;

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, trackChanges)).ReturnsAsync(_user);
            _mapperMock.Setup(x => x.Map<UserDto>(_user)).Returns(_userDto);

            // Act
            var result = await _userService.GetUserById(userId, trackChanges);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(_userDto);

            _repositoryManagerMock.Verify(x => x.UserRepository.GetUserAsync(userId, false), Times.Once);
            _mapperMock.Verify(x => x.Map<UserDto>(_user), Times.Once);
        }

        [Fact]
        public async Task GetUserById_WhenUserNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, It.IsAny<bool>())).ReturnsAsync((User)null!);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserById(userId, false));
        }

        [Fact]
        public async Task GetUserCollectionByIds_WhenUsersExists_ReturnsCollectionUserDto()
        {
            // Arrange
            var trackChanges = false;
            IEnumerable<Guid> userIds = new[] { Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), Guid.Parse("b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e"), Guid.Parse("c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f") };

            _repositoryManagerMock.Setup(x => x.UserRepository.GetByIdsAsync(userIds, trackChanges)).ReturnsAsync(_users.AsEnumerable);
            _mapperMock.Setup(x => x.Map<IEnumerable<UserDto>>(_users)).Returns(_usersDto);

            // Act
            var result = await _userService.GetUsersByIds(userIds, trackChanges);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Contain(_usersDto);

            _mapperMock.Verify(x => x.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>()), Times.Once);
            _repositoryManagerMock.Verify(x => x.UserRepository.GetByIdsAsync(userIds, trackChanges), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ShouldUpdateCorrectly_WhenIdAndModelProvided()
        {
            // Arrange
            var userId = Guid.Parse(_user.Id);
            var trackChanges = true;

            _mapperMock.Setup(x => x.Map(_userForUpdate, _user));
            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, trackChanges)).ReturnsAsync(_user).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _userService.UpdateUser(userId, _userForUpdate, trackChanges);

            // Assert
            _repositoryManagerMock.VerifyAll();
            _mapperMock.VerifyAll();
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteUser_ShouldDelete_WhenUserExists()
        {
            // Arrange
            var userId = Guid.Parse(_user.Id);
            bool trackChanges = false;

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, trackChanges)).ReturnsAsync(_user).Verifiable();
            _repositoryManagerMock.Setup(x => x.UserRepository.DeleteUser(_user));

            // Act
            await _userService.DeleteUser(userId, trackChanges);

            // Assert
            _repositoryManagerMock.Verify(x => x.UserRepository.DeleteUser(_user), Times.Once);
            _repositoryManagerMock.VerifyAll();

        }

        [Fact]
        public async Task UpdateUser_WhenUserNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, It.IsAny<bool>())).ReturnsAsync((User)null!);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.UpdateUser(userId, _userForUpdate, trackChanges: true));
        }

        [Fact]
        public async Task DeleteUser_WhenUserNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId, It.IsAny<bool>())).ReturnsAsync((User)null!);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() =>  _userService.DeleteUser(userId, trackChanges: false));
        }

        [Fact]
        public async Task GetUsersByIds_WhenIdsNull_ThrowsIdParametersBadRequestException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<IdParametersBadRequestException>(() =>  _userService.GetUsersByIds(null!, trackChanges: false));

            _repositoryManagerMock.Verify(x => x.UserRepository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>()), Times.Never);
        }

        [Fact]
        public async Task GetUsersByIds_WhenCountMismatch_ThrowsCollectionByIdsBadRequestException()
        {
            // Arrange
            var userIds = _users.Select(u => Guid.Parse(u.Id)).Take(3).ToList();
            var returnedUsers = _users.Take(2).ToList();

            _repositoryManagerMock.Setup(x => x.UserRepository.GetByIdsAsync(userIds, It.IsAny<bool>())).ReturnsAsync(returnedUsers);

            // Act & Assert
            await Assert.ThrowsAsync<CollectionByIdsBadRequestException>(() => _userService.GetUsersByIds(userIds, trackChanges: false));
        }
    }
}
