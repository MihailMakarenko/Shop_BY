namespace Entities.Exceptions.UsersException
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) : base($"The User with " +
          $"id: {userId} doesn't exist in the database.")
        {
        }
    }
}
