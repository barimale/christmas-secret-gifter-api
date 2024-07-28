using Christmas.Secret.Gifter.API.Behaviours;
using Christmas.Secret.Gifter.Application.Services;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Infrastructure;
using Christmas.Secret.Gifter.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Christmas.Secret.Gifter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<ILocalesStatusHub, LocalesStatusHub>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            services.AddSQLLiteDatabase();
            services.AddCors();

            services.AddDbContext<GifterDbContext>(options =>
                options
                    .UseSqlite(Configuration.GetConnectionString("GifterDbContext"),
                b => b.MigrationsAssembly(typeof(GifterDbContext).Assembly.FullName)));

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Christmas-Secret-Gifter-API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GifterDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception)
            {
                Console.WriteLine("On Migrate error");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GifterDbContext v1"));
            }

            app.UseRouting();
            app.UseHsts();

            app.UseCors(p =>
            {
                p.AllowAnyOrigin();
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                //p.WithOrigins("http://localhost:3008").AllowCredentials();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
