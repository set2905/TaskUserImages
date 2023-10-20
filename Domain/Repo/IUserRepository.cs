using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IUserRepository
    {
        Task<Result<User>> GetByIdAsync(UserId id);
        Task<Result<List<User>>> GetUsers(int page, int pageSize);
        Task<Result> Insert(User entity, CancellationToken cancellationToken = default);
    }
}
