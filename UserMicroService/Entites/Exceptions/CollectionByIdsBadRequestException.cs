using Entities.Exceptions;

namespace Entites.Exceptions
{
    public class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() : base("Collection count mismatch comparing to ids") { }
    }
}
