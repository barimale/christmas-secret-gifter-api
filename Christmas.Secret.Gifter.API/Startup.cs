using Christmas.Secret.Gifter.API.Behaviours;
using Christmas.Secret.Gifter.Application.Services;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Infrastructure;
using Christmas.Secret.Gifter.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Christmas.Secret.Gifter.API.Extensions;
using Microsoft.AspNetCore.HttpLogging;

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
            services.AddProblemDetails();

            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Christmas.Secret.Gifter.Application.Commands.GiftEventCommands.AddGiftEventCommand).Assembly));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            services.AddSQLLiteDatabase();
            services.AddCors();

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

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
            app.UseHttpLogging();

            app.UseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                {
                    var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
                }
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
