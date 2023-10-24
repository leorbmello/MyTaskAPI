namespace LeoMello.Application.DTOs.Response
{
    public record AuthModelResponse
    {
        public string? Token { get; init; }

        public DateTime? Expiration { get; init; }

        public AuthModelResponse(string token, DateTime expiration)
        {
            Token = token; 
            Expiration = expiration;
        }
    }
}
