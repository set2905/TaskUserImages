using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository=userRepository;
        }

        public async Task<Result> CreateUser(string userName)
        {
            User user = new(new(Guid.NewGuid()), userName);
            return await userRepository.Insert(user);
        }
        public async Task<Result<List<User>>> GetUsers(int page, int pageSize)
        {
            return await userRepository.GetUsers(page, pageSize);
        }
    }
}
