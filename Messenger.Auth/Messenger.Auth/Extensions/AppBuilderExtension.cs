using Messenger.Auth.Data;
using Messenger.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Auth.Extensions;

public static class AppBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<MessengerAuthDbContext>();

        if (!await dbContext.UserRoles.AnyAsync())
        {
            var roles = new List<UserRole>
            {
                new UserRole { Role = "Admin" },
                new UserRole { Role = "User" }
            };

            await dbContext.UserRoles.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
