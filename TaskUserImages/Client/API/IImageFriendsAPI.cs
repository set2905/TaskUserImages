using Contracts.Dto;
using Refit;

namespace TaskUserImages.Client.API
{
    public interface IImageFriendsAPI
    {
        [Get("/user/profiles")]
        Task<List<UserDto>> GetUserProfiles(int page, int pageSize);
        [Post("/images/upload")]
        Task UploadImage(UploadedFile file);
        [Get("/images/img")]
        Task<object> GetImage(Guid id);
        [Get("/images/userimages")]
        Task<List<string>> GetUserImageUrls(string userName);
    }
}
