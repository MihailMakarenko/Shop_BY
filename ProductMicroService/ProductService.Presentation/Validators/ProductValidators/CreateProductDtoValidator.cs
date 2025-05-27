using FluentValidation;
using Shared.DataTransferObjects.ProductDto;

namespace ProductService.Presentation.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<ProductForCreationDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(product => product.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(product => product.Description).NotEmpty().MaximumLength(500);
            RuleFor(product => product.Price).NotEmpty().GreaterThan(0.01m);
        }
    }
}
