using MediatR;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace Service.Queries.ProductQueries.GetProductForUser
{
    public class GetProductForUserHandler(IServiceManager _serviceManager) : IRequestHandler<GetProductForUserQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductForUserQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _serviceManager.ProductService.GetProductByUserAsync(request.id, request.UserId, trackChanges: false);

            return product;
        }
    }
}
