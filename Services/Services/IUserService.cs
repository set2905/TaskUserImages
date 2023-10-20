using Ardalis.Result;

namespace Services.Services
{
    public interface IUserService
    {
        Task<Result> CreateUser(string userName);
    }
}