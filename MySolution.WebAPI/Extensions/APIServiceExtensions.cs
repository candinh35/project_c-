using System.Reflection;
using Framework.Core.Abstractions;
using Framework.Core.Entities;
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
        services.AddScoped(typeof(IUnitOfWork), typeof(BaseUnitOfWork<CoreDbContext>));
        return services;
    }
}