using MediatR;
using Shared.DataTransferObjects.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands.ProductCommands.DeleteProduct
{
    public record DeleteProductCommand(Guid UserId, Guid id) : IRequest<Unit>;

}
