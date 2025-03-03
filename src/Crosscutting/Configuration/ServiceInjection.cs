using Domain.Interface.Services.Auth;
using Domain.Interface.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Services.Auth;
using Service.Services.User;

namespace Crosscutting.Configuration
{
    public static class ServiceInjection
    {
        public static IServiceCollection Execute(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}