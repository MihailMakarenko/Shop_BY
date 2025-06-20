using Contracts;
using Entities.Models;
using MediatR;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;

namespace Service.Commands.ProductCommands.CreateProduct
{

    public class CreateProductHandler(IServiceManager _serviceManager) : IRequestHandler<CreateProductCommand, ProductDto> 
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _serviceManager.ProductService.CreateProductForUser(request.UserId, request.ProductForCreation, trackChanges: false);
        }
    }

}
