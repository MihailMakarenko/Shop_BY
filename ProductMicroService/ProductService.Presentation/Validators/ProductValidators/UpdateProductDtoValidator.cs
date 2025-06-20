using FluentValidation;
using Service.Commands.ProductCommands.UpdateProduct;

namespace ProductService.Presentation.Validators.ProductValidators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(product => product.ProductForUpdate.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(product => product.ProductForUpdate.Description).NotEmpty().MinimumLength(3).MaximumLength(500);
            RuleFor(product => product.ProductForUpdate.Price).NotEmpty().GreaterThan(0);
        }
    }
}
