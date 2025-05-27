using Entities.Exceptions;

namespace Entites.Exceptions.UsersException
{
    public class UserNotFoundByEmailException : NotFoundException
    {
        public UserNotFoundByEmailException(string email) : base($"The User with " +
          $"email: {email} doesn't exist in the database.")
        {
        }
    }
}
