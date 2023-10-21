using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository=userRepository;
        }

        public async Task<Result> CreateUserProfile(string userName, string id)
        {
            User user = new(new(Guid.NewGuid()), userName, id);
            return await userRepository.Insert(user);
        }
        public async Task<Result<List<User>>> GetUserProfiles(int page, int pageSize)
        {
            return await userRepository.GetUsers(page, pageSize);
        }

    }
}
