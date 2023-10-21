using Contracts.Dto;
using Refit;

namespace TaskUserImages.Client.API
{
    public interface IImageFriendsAPI
    {
        [Get("/User/GetUserProfiles")]
        Task<List<UserDto>> GetUserProfiles(int page, int pageSize);
        [Post("/Image/Upload")]
        Task UploadImage(UploadedFile file);
    }
}
