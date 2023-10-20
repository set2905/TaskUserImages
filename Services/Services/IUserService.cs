using Ardalis.Result;
using Domain.Entities;

namespace Services.Services
{
    public interface IUserService
    {
        Task<Result> CreateUser(string userName);
        Task<Result<List<User>>> GetUsers(int page, int pageSize);
    }
}