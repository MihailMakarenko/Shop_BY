using MediatR;
using Shared.DataTransferObjects.ProductDto;


namespace Service.Commands.ProductCommands.CreateProduct
{
    public record CreateProductCommand(Guid UserId, ProductForCreationDto ProductForCreation) : IRequest<ProductDto>;

}
