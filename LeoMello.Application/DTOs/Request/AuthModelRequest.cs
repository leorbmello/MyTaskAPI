namespace LeoMello.Application.DTOs.Request
{
    public record AuthModelRequest
    {
        public string? Username { get; init; }
        public string? Password { get; init; }
    }
}
