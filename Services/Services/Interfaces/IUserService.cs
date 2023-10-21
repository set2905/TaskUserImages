using Ardalis.Result;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUserProfile(string userName);
        Task<Result<List<User>>> GetUserProfiles(int page, int pageSize);
    }
}