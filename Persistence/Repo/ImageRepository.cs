using Ardalis.Result;
using Domain.Entities;
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
                if (result == null) return Result.NotFound($"Image with id {id.Value} is not found");
                return Result.Success(result);
            }
        }


    }
}
