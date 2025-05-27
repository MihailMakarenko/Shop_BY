using Entities.Exceptions;

namespace Entites.Exceptions
{
    public class IdParametrsBadRequestException : BadRequestException
    {
        public IdParametrsBadRequestException() : base("Parameter ids is null") { }
    }
}
