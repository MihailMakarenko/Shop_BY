using MediatR;
using Service.Commands.ProductCommands.CreateProduct;
using Service.Contract;
using Shared.DataTransferObjects.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands.ProductCommands.UpdateProduct
{
    public class UpdateProductHandler(IServiceManager _serviceManager) : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _serviceManager.ProductService.UpdateProductForUser(request.id,request.UserId, request.ProductForUpdate, trackChanges: true);

            return Unit.Value;
        }

    }
}
