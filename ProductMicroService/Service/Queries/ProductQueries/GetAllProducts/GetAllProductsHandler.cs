using MediatR;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace Service.Queries.ProductQueries.GetAllProducts
{
    public class GetAllProductsHandler(IServiceManager _serviceManager) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var products = await _serviceManager.ProductService.GetAllProducts(request.sieveModel, trackChanges: false);

            return products;
        }
    }
}
