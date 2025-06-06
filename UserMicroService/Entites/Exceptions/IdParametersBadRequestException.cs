using Entities.Exceptions;

namespace Entites.Exceptions
{
    public class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Parameter ids is null") { }
    }
}
