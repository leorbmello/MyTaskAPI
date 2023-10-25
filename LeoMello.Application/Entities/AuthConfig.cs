using LeoMello.Application.Interfaces;

namespace LeoMello.Application.Entities
{
    public class AuthConfig : IAuthConfig
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int Expiration { get; set; }
    }
}
