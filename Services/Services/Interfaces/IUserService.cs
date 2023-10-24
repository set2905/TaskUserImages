using Ardalis.Result;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUserProfile(string userName, string id);
        Task<Result<List<User>>> GetUserProfiles(int skip, int take);
    }
}