namespace Entities.Exceptions.UsersException
{
    public class UsersNotFoundException : NotFoundException
    {
        public UsersNotFoundException(Guid userId) : base($"The User with " +
          $"id: {userId} doesn't exist in the database.")
        {
        }
    }
}
