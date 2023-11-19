namespace LeoMello.Application.DTOs.Request
{
    public record CreateUserModelRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string Role { get; init; }
    }
}
