using MediatR;
using Shared.DataTransferObjects.ProductDto;
using Sieve.Models;

namespace Service.Queries.ProductQueries.GetAllProducts
{
    public record GetAllProductsQuery(SieveModel sieveModel) : IRequest<IEnumerable<ProductDto>>;
}
