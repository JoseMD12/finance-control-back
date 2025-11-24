
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class Configure
    {
        public static void Execute(IServiceCollection services)
        {
            services.AddTransient(typeof(IBase<>), typeof(Base<>));
            services.AddTransient<IUser, User>();
        }
    }
}