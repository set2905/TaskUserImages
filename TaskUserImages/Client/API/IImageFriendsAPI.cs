using Contracts.Dto;
using Refit;

namespace TaskUserImages.Client.API
{
    public interface IImageFriendsAPI
    {
        [Get("/User/GetUsers")]
        Task<List<UserDto>> GetUsers(int page, int pageSize);
    }
}
