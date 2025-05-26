using FluentValidation;
using Shared.DataTransferObjects.UserDto;

namespace UserService.Presentation.Validators.UserValidators
{
    public class UserForUpdateValidator : AbstractValidator<UserForUpdateDto>
    {
        public UserForUpdateValidator()
        {
            RuleFor(user => user.FirstName).ApplyFirstNameRules();

            RuleFor(user => user.LastName).ApplyLastNameRules();

            RuleFor(user => user.Email).ApplyEmailRules();

            RuleFor(r => r.PhoneNumber).ApplyPhoneRules();

            RuleFor(user => user.Password).ApplyPasswordRules();

        }
    }
}
