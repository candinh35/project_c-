using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Model.Core;

namespace MySolution.WebAPI.Extensions;

public static class APIServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectString = configuration.GetConnectionString("CoreDb");
        var migrationAssemble = typeof(CoreDbContext).GetTypeInfo().Assembly.GetName().Name;
        services.AddDbContext<CoreDbContext>(builder =>
        {
            builder.UseNpgsql(connectString, sql => sql.MigrationsAssembly(migrationAssemble));
            builder.EnableSensitiveDataLogging();
            builder.LogTo(Console.WriteLine);
        });

        return services;
    }
}