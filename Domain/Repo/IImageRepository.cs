using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IImageRepository
    {
        Task<Result<Image>> GetByIdAsync(ImageId id);
        Task<Result> InsertAsync(Image entity, CancellationToken cancellationToken = default);
    }
}
