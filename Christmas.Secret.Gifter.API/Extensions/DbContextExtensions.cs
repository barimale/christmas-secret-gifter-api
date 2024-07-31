using Christmas.Secret.Gifter.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Christmas.Secret.Gifter.API.Extensions
{
    public static class DbContextExtensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                using var dbContext = scope.ServiceProvider.GetRequiredService<GifterDbContext>();
                dbContext.Database.MigrateAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return app;
        }
    }
}
