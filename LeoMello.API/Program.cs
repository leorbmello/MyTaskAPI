using LeoMello.Application.Entities;
using LeoMello.IoC;
using LeoMello.Shared.Exceptions.Configuration;
using LeoMello.Shared.Exceptions.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace LeoMello.API
{
    public class Program
    {
        // assembly version
        public static string Version => Assembly.GetEntryAssembly()?
                                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                                .InformationalVersion ?? "0.0.0.0";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServicesCollection(builder.Configuration);

            // Add service controllers
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = builder.Configuration["Swagger:Description"],
                    Version = Version,
                    Contact = new OpenApiContact
                    {
                        Email = builder.Configuration["Swagger:Contact:Email"],
                        Name = builder.Configuration["Swagger:Contact:Name"]
                    }
                });
            });

            var app = builder.Build();
            
            // validate configuration
            var authConfig = app.Services.GetRequiredService<IOptions<AuthConfig>>().Value;
            if (authConfig is null)
            {
                throw new ConfigurationException(new ExceptionErrorMessage(ExceptionCode.AuthConfigMissing, "The AuthConfig is missing on app settings!"));
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}