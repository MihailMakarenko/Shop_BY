using MediatR;
using Shared.DataTransferObjects.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands.ProductCommands.UpdateProduct
{
    public record UpdateProductCommand(Guid id,Guid UserId, ProductForUpdateDto ProductForUpdate) : IRequest<Unit>;
}
