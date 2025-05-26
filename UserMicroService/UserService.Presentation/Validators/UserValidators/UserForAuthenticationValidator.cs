using FluentValidation;

namespace UserService.Presentation.Validators.UserValidators
{
    public class UserForAuthenticationValidator : AbstractValidator<Shared.DataTransferObjects.UserDto.UserForAuthenticationDto>
    {
        public UserForAuthenticationValidator()
        {
            RuleFor(u => u.Email).ApplyEmailRules();

            RuleFor(u => u.Password).ApplyPasswordRules();
        }
    }
}
