using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions.EmailException
{
    public class InvalidEmailConfirmationTokenException : Exception
    {
        public InvalidEmailConfirmationTokenException() : base("Invalid token.") { }
    }

}
