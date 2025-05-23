using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class IdParametrsBadRequestException : BadRequestException
    {
        public IdParametrsBadRequestException() : base("Parameter ids is null") { }
    }
}
