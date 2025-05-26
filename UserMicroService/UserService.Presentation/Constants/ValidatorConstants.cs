namespace UserService.Presentation.Constants
{
    public sealed class ValidatorConstants
    {
        public const string NotEmpty = "{PropertyName} is required";
        public const string NotNull = "{PropertyName} cannot be null";
        public const string MaximumLength = "{PropertyName} cannot exceed {MaxLength} characters.";
        public const string EmailAddress = "{PropertyName} must be a valid email address.";
        public const string PhoneNumber = "Phone number must be in the format: 25XXXXXXX, 33XXXXXXX, or 44XXXXXXX (9 digits total).";
    }
}
