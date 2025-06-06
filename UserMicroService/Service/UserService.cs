using AutoMapper;
using Contracts;
using Entites.Exceptions;
using Entities.Exceptions.UsersException;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;

namespace Service
{
    public class UserService(IRepositoryManager _repositoryManager, IMapper _mapper, IRabbitMqService _mqService) : IUserService
    {
        public async Task DeactivateUser(Guid userId, bool trackChanges)
        {
            var userEntity = await GetAndCheckUserIfExists(userId, trackChanges);

            userEntity.LockoutEnabled = true;

            await _mqService.SendMessage(userId.ToString());

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteUser(Guid userId, bool trackChanges)
        {
            var user = await GetAndCheckUserIfExists(userId, trackChanges);

            _repositoryManager.UserRepository.DeleteUser(user);
            await _repositoryManager.SaveAsync();
        }

        public async Task<UserDto> GetUserById(Guid id, bool trackChanges)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(id, trackChanges);

            if (user == null)
                throw new UserNotFoundException(id);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids == null)
                throw new IdParametersBadRequestException();

            var usersEntitie = await _repositoryManager.UserRepository.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != usersEntitie.Count())
                throw new CollectionByIdsBadRequestException();

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(usersEntitie);

            return usersToReturn;
        }

        public async Task UpdateUser(Guid userId, UserForUpdateDto userForUpdate, bool trackChanges)
        {
            var userEntity = await GetAndCheckUserIfExists(userId, trackChanges);

            _mapper.Map(userForUpdate, userEntity);
            await _repositoryManager.SaveAsync();
        }

        private async Task<User> GetAndCheckUserIfExists(Guid userId, bool trackChanges)
        {
            var userEntity = await _repositoryManager.UserRepository.GetUserAsync(userId, trackChanges);
            if (userEntity is null)
                throw new UserNotFoundException(userId);

            return userEntity;
        }

    }
}
