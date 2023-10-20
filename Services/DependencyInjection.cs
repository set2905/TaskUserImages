using Microsoft.Extensions.DependencyInjection;
using Persistence.Repo;
using Services.Services;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
