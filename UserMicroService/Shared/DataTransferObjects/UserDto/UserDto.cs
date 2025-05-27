namespace Shared.DataTransferObjects.UserDto
{
    public record UserDto
    {
        public Guid Id { get; init; }

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

        public string? FullName { get; init; }
    }
}
