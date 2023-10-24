using LeoMello.Application.DTOs.Request;
using LeoMello.Application.DTOs.Response;
using LeoMello.Application.Entities;
using LeoMello.Application.Interfaces;
using LeoMello.DAL;
using LeoMello.DAL.Entities;
using LeoMello.Shared.Exceptions.Authorization;
using LeoMello.Shared.Exceptions.Configuration;
using LeoMello.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LeoMello.Application.Services
{
    public class AuthService : IAuthService
    {
        private const int AUTHTOKEN_EXPIRATION = 300; // in seconds

        private readonly AuthConfig tokenConfig;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public AuthService(
            IConfiguration configuration,
            ApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;

            // bind the config section
            configuration.GetSection("AuthConfig").Bind(tokenConfig);
        }

        public async Task<AuthModelResponse> AuthenticateAsync(AuthModelRequest request)
        {
            if (request.Username is null)
            {
                throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthInvalidUser, "Username can`t be null!"));
            }

            var user = await userManager.FindByNameAsync(request.Username);
            if (user is null)
            {
                throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthUnknownUser, "User not found!"));
            }

            var signinResult = await signInManager.PasswordSignInAsync(
                user,
                request.Password,
                false,
                lockoutOnFailure: false
            );

            if (signinResult.Succeeded)
            {
                return new AuthModelResponse(CreateToken(user), DateTime.Now.AddSeconds(AUTHTOKEN_EXPIRATION));
            }

            throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthFailed, "Failed to authenticate!"));
        }

        private string CreateToken(ApplicationUser user)
        {
            var claims = dbContext.UserClaims.
                Where(x => x.UserId == user.Id)
                .Select(x => x.ToClaim())
                .ToList();

            if (tokenConfig is null)
            {
                throw new ConfigurationException(new ExceptionErrorMessage(ExceptionCode.AuthConfigMissing, "Missing AuthConfig information!"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key)); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(AUTHTOKEN_EXPIRATION),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            if (tokenString is null)
            {
                throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthTokenCreationFailed, "Failed to generate the token!"));
            }

            return tokenString;
        }
    }
}
