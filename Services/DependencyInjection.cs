using Domain.Repo;
using FileSignatures;
using FileSignatures.Formats;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repo;
using Services.Services;
using Services.Services.Interfaces;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            IEnumerable<Image> recognised = FileFormatLocator.GetFormats().OfType<Image>();
            FileFormatInspector inspector = new FileFormatInspector(recognised);
            services.AddSingleton<IFileFormatInspector>(inspector);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>(pv=>new("C:\\Users\\user\\Downloads",
                                                                    pv.GetService<IImageRepository>()!,
                                                                    pv.GetService<IUserProfileRepository>()!,
                                                                    pv.GetService<IFileFormatInspector>()!));

            return services;
        }
    }
}
