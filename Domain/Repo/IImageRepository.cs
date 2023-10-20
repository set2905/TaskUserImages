using Domain.Entities;

namespace Domain.Repo
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(ImageId id);
        Task<ImageId> Save(Image entity, CancellationToken cancellationToken = default);
    }
}
