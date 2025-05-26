using FluentValidation;
using System.Text.RegularExpressions;
using UserService.Presentation.Constants;

namespace UserService.Presentation.Validators.UserValidators
{
    internal static class UserValidatorRules
    {
        public static IRuleBuilderOptions<T, string?> ApplyFirstNameRules<T>(this IRuleBuilder<T, string?> rule)
        {
            return rule
                 .NotEmpty().WithMessage(ValidatorConstants.NotEmpty)
                 .MaximumLength(50).WithMessage(ValidatorConstants.MaximumLength);
        }

        public static IRuleBuilderOptions<T, string?> ApplyLastNameRules<T>(this IRuleBuilder<T, string?> rule)
        {
            return rule
                .NotEmpty().WithMessage(ValidatorConstants.NotEmpty)
                .MaximumLength(50).WithMessage(ValidatorConstants.MaximumLength);
        }

        public static IRuleBuilderOptions<T, string?> ApplyEmailRules<T>(this IRuleBuilder<T, string?> rule)
        {
            return rule
              .NotEmpty().WithMessage(ValidatorConstants.NotEmpty)
              .EmailAddress().WithMessage(ValidatorConstants.EmailAddress)
              .MaximumLength(254).WithMessage(ValidatorConstants.MaximumLength);
        }

        public static IRuleBuilderOptions<T, string?> ApplyPhoneRules<T>(this IRuleBuilder<T, string?> rule)
        {
            return rule
                  .NotEmpty().WithMessage(ValidatorConstants.NotEmpty)
                  .Matches(@"^(25|33|44)\d{7}$").WithMessage(ValidatorConstants.PhoneNumber);
        }

        public static IRuleBuilderOptions<T, string?> ApplyPasswordRules<T>(this IRuleBuilder<T, string?> rule)
        {
            return rule
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(32).WithMessage("Password cannot exceed 32 characters.")
                .Must(ContainDigit).WithMessage("Password must contain at least one digit (0-9).")
                .Must(ContainLowercase).WithMessage("Password must contain at least one lowercase letter (a-z).")
                .Must(ContainUppercase).WithMessage("Password must contain at least one uppercase letter (A-Z).")
                .Must(ContainSpecialCharacter).WithMessage("Password must contain at least one special character (!@#$%^&*).");
        }

        private static bool ContainDigit(string? password) =>
            !string.IsNullOrEmpty(password) && Regex.IsMatch(password, "[0-9]");

        private static bool ContainLowercase(string? password) =>
            !string.IsNullOrEmpty(password) && Regex.IsMatch(password, "[a-z]");

        private static bool ContainUppercase(string? password) =>
            !string.IsNullOrEmpty(password) && Regex.IsMatch(password, "[A-Z]");

        private static bool ContainSpecialCharacter(string? password) =>
            !string.IsNullOrEmpty(password) && Regex.IsMatch(password, "[!@#$%^&*]");
    }
}
