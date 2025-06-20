using MediatR;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace Service.Queries.ProductQueries.GetProductsForUser
{
    public class GetProductsForUserHandler(IServiceManager _serviceManager) : IRequestHandler<GetProductsForUserQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsForUserQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var products = await _serviceManager.ProductService.GetProductsByUserAsync(request.userId, request.sieveModel, trackChanges: false);

            return products;
        }
    }
}
