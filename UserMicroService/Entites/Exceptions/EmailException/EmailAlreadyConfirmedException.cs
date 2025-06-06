using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions.EmailException
{
    public class EmailAlreadyConfirmedException : Exception
    {
        public EmailAlreadyConfirmedException() : base("Email is already confirmed.") { }
    }
}
