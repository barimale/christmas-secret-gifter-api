using Christmas.Secret.Gifter.Database.SQLite.Configuration;
using Christmas.Secret.Gifter.Database.SQLite.SQLite.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Christmas.Secret.Gifter.Database.SQLite.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSQLLiteDatabase(this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            //WIP check it
            services.AddSQLLiteDatabaseAutoMapper();

            return services;
        }

        public static IServiceCollection AddSQLLiteDatabaseAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappings));

            return services;
        }
    }
}
