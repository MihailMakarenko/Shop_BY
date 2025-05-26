using FluentValidation;
using Shared.DataTransferObjects.UserDto;

namespace UserService.Presentation.Validators.UserValidators
{
    public class UserForCreationValidator : AbstractValidator<UserForCreationDto>
    {
        public UserForCreationValidator()
        {
            RuleFor(user => user.FirstName).ApplyFirstNameRules();

            RuleFor(user => user.LastName).ApplyLastNameRules();

            RuleFor(user => user.Email).ApplyEmailRules();

            RuleFor(user => user.PhoneNumber).ApplyPhoneRules();

            RuleFor(user => user.Password).ApplyPasswordRules();
        }
    }
}
