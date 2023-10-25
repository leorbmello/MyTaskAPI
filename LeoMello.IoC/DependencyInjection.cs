using LeoMello.Application.Entities;
using LeoMello.Application.Interfaces;
using LeoMello.DAL;
using LeoMello.DAL.Entities;
using LeoMello.IoC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LeoMello.IoC
{
    public static class DependencyInjection
    {
        /// <summary>
        ///     When we need to impement new services or configurations to the main application, we set the changes here,
        ///     this way we dont need to keep changing things on the Program.cs.
        /// </summary>
        public static IServiceCollection AddServicesCollection(this IServiceCollection services, IConfiguration configuration)
        {
            // set the database context information to be used by our application
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            services.Configure<AuthConfig>(configuration.GetSection("AuthConfig"));

            // add sigin manager
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // set the authentication information
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["AuthConfig:Issuer"],
                    ValidAudience = configuration["AuthConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Key"] ?? throw new Exception()))
                };
            });

            // add services injections
            services.AddServices(configuration);

            return services;
        }
    }
}