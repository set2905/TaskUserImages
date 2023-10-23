using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IImageRepository
    {
        Task<Result<Image>> GetByIdAsync(ImageId id);
        Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(UserId id);
        Task<Result> InsertAsync(Image entity, CancellationToken cancellationToken = default);
    }
}
