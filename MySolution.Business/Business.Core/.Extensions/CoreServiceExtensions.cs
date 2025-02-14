using Business.Core.Interfaces;
using Business.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Core.Extensions;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddCoreService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsers, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}