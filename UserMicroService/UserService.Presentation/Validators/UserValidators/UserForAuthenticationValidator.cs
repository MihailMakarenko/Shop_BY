using FluentValidation;
using Shared.DataTransferObjects.UserDto;

namespace UserService.Presentation.Validators.UserValidators
{
    public class UserForAuthenticationValidator : AbstractValidator<UserForAuthenticationDto>
    {
        public UserForAuthenticationValidator()
        {
            RuleFor(u => u.Email).ApplyEmailRules();

            RuleFor(u => u.Password).ApplyPasswordRules();
        }
    }
}
