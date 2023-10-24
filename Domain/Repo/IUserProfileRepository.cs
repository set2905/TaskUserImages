using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IUserProfileRepository
    {
        Task<Result<User>> GetByIdAsync(UserId id);
        /// <summary>
        /// Finds user profile by ASP identity id
        /// </summary>
        /// <param name="identityId"></param>
        /// <returns></returns>
        Task<Result<User>> GetByIdentityAsync(string identityId);
        /// <summary>
        /// Finds user profile by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<User>> GetByUserNameAsync(string userName);
        /// <summary>
        /// Gets user friends
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<List<User>>> GetFriends(UserId userId);
        /// <summary>
        /// Gets users page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Result<List<User>>> GetUsersAsync(int page, int pageSize);
        Task<Result> InsertAsync(User entity, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(User entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Checks if user with <paramref name="userId"/> has a friend with <paramref name="friendId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        Task<Result<bool>> IsInFriendlist(UserId userId, UserId friendId);
    }
}
