namespace LeoMello.Application.Interfaces
{
    public interface IAuthConfig
    {
        string Issuer { get; }
        string Audience { get; }
        string Key { get; }
    }
}
