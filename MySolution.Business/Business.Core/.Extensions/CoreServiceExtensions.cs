using Business.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Core.Extensions;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddCoreService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsers, UserService>();
        
        return services;
    }
}