using Discogs.Api.Core.Repositories;
using Discogs.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Discogs.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureContainer(this IServiceCollection services)
        {
            services.AddSingleton<IDiscogsRepository, DiscogsRepository>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    // Allow camel casing in JSON property names
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Discogs.Api",
                    Version = "v1",
                    Description = "ASP.NET Core 5.0 API project to wrap Discogs API"
                });
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
