using MediatR;
using Shared.DataTransferObjects.ProductDto;

namespace Service.Queries.ProductQueries.GetProductForUser
{
    public record GetProductForUserQuery(Guid id, Guid UserId) : IRequest<ProductDto>;
}
