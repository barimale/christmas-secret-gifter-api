using Christmas.Secret.Gifter.Infrastructure.Repositories;
using Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Christmas.Secret.Gifter.Infrastructure.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSQLLiteDatabase(this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IParticipantRepository, ParticipantRepository>();

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
