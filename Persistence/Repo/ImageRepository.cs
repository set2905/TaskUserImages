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

        public override Task<Image?> GetByIdAsync(ImageId id)
        {
            throw new NotImplementedException();
        }

        public override Task<ImageId> Save(Image entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
