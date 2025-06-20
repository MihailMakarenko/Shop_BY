using MediatR;
using Service.Commands.ProductCommands.UpdateProduct;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands.ProductCommands.DeleteProduct
{

    public class DeleteProductHandler(IServiceManager _serviceManager) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _serviceManager.ProductService.DeleteProductForUser(request.id, request.UserId, trackChanges: false);

            return Unit.Value;
        }

    }
}
