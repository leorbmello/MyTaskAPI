using LeoMello.Application.DTOs.Request;
using LeoMello.Application.DTOs.Response;
using LeoMello.Application.Entities;
using LeoMello.Application.Interfaces;
using LeoMello.DAL;
using LeoMello.DAL.Entities;
using LeoMello.Shared.Exceptions.Authorization;
using LeoMello.Shared.Exceptions.Configuration;
using LeoMello.Shared.Exceptions.Creation;
using LeoMello.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LeoMello.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthConfig tokenConfig;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public AuthService(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;

            int.TryParse(configuration["AuthConfig:Expiration"], out int expiration);
            if (expiration == 0)
            {
                expiration = 300;
            }

            tokenConfig = new AuthConfig()
            {
                Audience = configuration["AuthConfig:Audience"],
                Expiration = expiration,
                Issuer = configuration["AuthConfig:Issuer"],
                Key = configuration["AuthConfig:Key"]
            };
        }

        /// <summary>
        ///     WARNING: This is a test code, remove it before release!
        /// </summary>
        public async Task<CreateUserModelResponse> CreateUserAsync(CreateUserModelRequest request)
        {
            var exist = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(request.Username));
            if (exist != null)
            {
                throw new CreationException(new ExceptionErrorMessage(ExceptionCode.UserAlreadyExist, $"{request.Username} already exists!"));
            }

            var newUser = new ApplicationUser 
            {
                UserName = request.Username, 
                CreationDate = DateTime.Now, 
            };

            var createResult = await userManager.CreateAsync(newUser, request.Password);
            if (createResult.Succeeded)
            {
                if (!await dbContext.CreateAsync(new ApplicationUserClaim() { UserId = newUser.Id, ClaimType = "Role", ClaimValue = request.Role }))
                {
                    throw new CreationException(new ExceptionErrorMessage(ExceptionCode.UserRoleAssingFail, $"{request.Username} could not receive the role!"));
                }

                return new CreateUserModelResponse() { Message = "Registration Ok!" };
            }

            // yes, null, an error during creation.
            return null;
        }

        public async Task<AuthModelResponse> AuthenticateAsync(AuthModelRequest request)
        {
            if (request.Username is null)
            {
                throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthInvalidUser, "Username can`t be null!"));
            }

            if (tokenConfig is null)
            {
                throw new ConfigurationException(new ExceptionErrorMessage(ExceptionCode.AuthConfigMissing, "Missing AuthConfig information!"));
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
                return new AuthModelResponse(CreateToken(user), DateTime.Now.AddSeconds(tokenConfig.Expiration));
            }

            throw new AuthorizationException(new ExceptionErrorMessage(ExceptionCode.AuthFailed, "Failed to authenticate!"));
        }

        private string CreateToken(ApplicationUser user)
        {
            var claims = dbContext.UserClaims.
                Where(x => x.UserId == user.Id)
                .Select(x => x.ToClaim())
                .ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key)); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(tokenConfig.Expiration),
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
