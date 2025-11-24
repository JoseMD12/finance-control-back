using Application.Interfaces.Auth;
using Application.Service.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class Configure
    {
        public static void Execute(IServiceCollection services)
        {
            services.AddTransient<ILogin, Login>();
            services.AddTransient<IDecodeBasicAuth, DecodeBasicAuth>();
        }
    }
}