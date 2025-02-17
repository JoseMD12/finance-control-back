using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Crosscutting.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            return services;
        }
    }
}