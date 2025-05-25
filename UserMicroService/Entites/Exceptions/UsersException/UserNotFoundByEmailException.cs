using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
