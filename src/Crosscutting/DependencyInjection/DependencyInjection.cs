using Data.Context;
using Data.Repositories;
using Domain.Interface.Repositories;
using Domain.Interface.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Service.Services.User;

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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddTransient<IUserService, UserService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddSingleton(x =>
               new PostgresDbContextFactory(configuration));

            return services;
        }
    }
}