using Discogs.Api.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Description = "ASP.NET Core 2.0 API project to wrap Discogs API"
                });
            });
        }
    }
}
