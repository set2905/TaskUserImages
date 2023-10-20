using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IUserRepository
    {
        Task<Result<User>> GetByIdAsync(UserId id);

        Task<Result> Insert(User entity, CancellationToken cancellationToken = default);
    }
}
