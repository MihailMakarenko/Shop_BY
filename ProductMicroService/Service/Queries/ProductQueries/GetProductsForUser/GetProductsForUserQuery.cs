using MediatR;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;

namespace Service.Queries.ProductQueries.GetProductsForUser
{
    public record GetProductsForUserQuery(Guid userId, SieveModel sieveModel) : IRequest<IEnumerable<ProductDto>>;
}
