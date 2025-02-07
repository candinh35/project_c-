using Microsoft.EntityFrameworkCore;
using Model.Core;

namespace MySolution.WebAPI.Extensions;

public static class DbInitializer
{
    public static IApplicationBuilder ConfigurationDb(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CoreDbContext>();
        context?.Database.Migrate();

        return app;
    }
}