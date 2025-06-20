using FluentValidation;
using Service.Commands.ProductCommands.CreateProduct;
using Shared.DataTransferObjects.ProductDto;

namespace ProductService.Presentation.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(product => product.ProductForCreation.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(product => product.ProductForCreation.Description).NotEmpty().MaximumLength(500);
            RuleFor(product => product.ProductForCreation.Price).NotEmpty().GreaterThan(0.01m);
        }
    }
}
