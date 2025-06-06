using Shared.DataTransferObjects.UserDto;

namespace Service.Contract
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(Guid id, bool trackChanges);
        Task<IEnumerable<UserDto>> GetUsersByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task DeleteUser(Guid userId, bool trackChanges);
        Task UpdateUser(Guid userId, UserForUpdateDto userForUpdate, bool trackChanges);
        Task DeactivateUser(Guid userId, bool trackChanges);
    }
}
