using LeoMello.Application.DTOs.Request;
using LeoMello.Application.DTOs.Response;

namespace LeoMello.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModelResponse> AuthenticateAsync(AuthModelRequest request);
        Task<CreateUserModelResponse> CreateUserAsync(CreateUserModelRequest request);
    }
}
