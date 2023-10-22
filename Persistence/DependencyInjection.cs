using Domain.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repo;

namespace Persistence
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mainConnectionString = configuration.GetConnectionString("MainConnection") ?? throw new InvalidOperationException("Connection string 'MainConnection' not found.");
            
            services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer(mainConnectionString)
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)));

            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUserProfileRepository, UserRepository>();
            services.AddScoped<IFriendshipRequestRepository, FriendshipRequestRepository>();

            return services;
        }
    }
}
