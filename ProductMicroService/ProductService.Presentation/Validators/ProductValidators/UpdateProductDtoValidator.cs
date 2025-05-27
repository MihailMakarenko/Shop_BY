using FluentValidation;
using Shared.DataTransferObjects.ProductDto;

namespace ProductService.Presentation.Validators.ProductValidators
{
    public class UpdateProductDtoValidator : AbstractValidator<ProductForUpdateDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(product => product.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(product => product.Description).NotEmpty().MinimumLength(3).MaximumLength(500);
            RuleFor(product => product.Price).NotEmpty().GreaterThan(0);
        }
    }
}
