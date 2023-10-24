using LeoMello.Application.Interfaces;
using LeoMello.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeoMello.IoC.Services
{
    public static class ServicesInjections
    {
        /// <summary>
        ///     Set our services to be claimed by the main application, so we inject them on the main scope,
        ///     and when they are requested, handle the services according to the base definitions.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
