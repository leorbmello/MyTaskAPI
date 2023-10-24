using LeoMello.Application.Interfaces;

namespace LeoMello.Application.Entities
{
    public class AuthConfig : IAuthConfig
    {
        public string Issuer { get; private set; }

        public string Audience { get; private set; }

        public string Key { get; private set; }
    }
}
