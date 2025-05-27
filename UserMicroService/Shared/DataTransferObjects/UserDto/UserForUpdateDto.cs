namespace Shared.DataTransferObjects.UserDto
{
    public record UserForUpdateDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Password { get; init; }
    }
}
