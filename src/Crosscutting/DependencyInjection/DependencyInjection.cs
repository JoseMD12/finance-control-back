using System.Reflection.Metadata;
using Data.Context;
using Domain.Interface.Services.Teste;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Service.Services.Teste;

namespace Crosscutting.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Finance Control API",
                        Version = "v1",
                        Description =
                            "API developed by José Henrique Martins Dotta. It is a simple API to control your finances.",
                        Contact = new OpenApiContact
                        {
                            Name = "José Henrique Martins Dotta",
                            Email = "josehmd.dev@gmail.com",
                        },

                    }
                );
            });

            services.AddCors(x =>
                x.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.AllowCredentials();
                })
            );

            services.AddTransient<ITesteService, TesteService>();

            services.AddSingleton(x =>
               new PostgresDbContextFactory(configuration));

            return services;
        }
    }
}