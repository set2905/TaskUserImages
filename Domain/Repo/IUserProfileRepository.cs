using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IUserProfileRepository
    {
        Task<Result<User>> GetByIdAsync(UserId id);
        Task<Result<User>> GetByIdentityAsync(string identityId);
        Task<Result<User>> GetByUserNameAsync(string userName);
        Task<Result<List<User>>> GetFriends(UserId userId);
        Task<Result<List<User>>> GetUsersAsync(int page, int pageSize);
        Task<Result> InsertAsync(User entity, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(User entity, CancellationToken cancellationToken = default);

        Task<Result<bool>> IsInFriendlist(UserId userId, UserId friendId);
    }
}
