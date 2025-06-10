using System.Reflection;
using CliniHub.Application.Features.Authentication;
using CliniHub.Application.Features.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CliniHub.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registrar serviços de aplicação aqui
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}