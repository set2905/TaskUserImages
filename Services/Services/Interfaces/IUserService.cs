using Ardalis.Result;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Creates new user profile and associates it with ASP identity <paramref name="id"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> CreateUserProfile(string userName, string id);
        /// <summary>
        /// Gets page of user profiles
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Result<List<User>>> GetUserProfiles(int skip, int take);
    }
}