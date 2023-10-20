using Domain.Entities;

namespace Domain.Repo
{
    public interface IUserRepository
    {

        Task<User?> GetByIdAsync(UserId id);

        Task<UserId> Save(User entity, CancellationToken cancellationToken = default);
    }
}
