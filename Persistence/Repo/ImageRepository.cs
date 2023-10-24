using Ardalis.Result;
using Domain.Entities;
using Domain.Errors;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public class ImageRepository : GenericRepository<Image, ImageId>, IImageRepository
    {
        public ImageRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public override async Task<Result<Image>> GetByIdAsync(ImageId id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<Image> images = context.Set<Image>();
                Image? result = await images.SingleOrDefaultAsync(x => x.Id == id);
                if (result == null) return DomainErrors.Image.NotFound;
                return Result.Success(result);
            }
        }


    }
}
