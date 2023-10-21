using Microsoft.Extensions.DependencyInjection;
using Persistence.Repo;
using Services.Services;
using Services.Services.Interfaces;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
