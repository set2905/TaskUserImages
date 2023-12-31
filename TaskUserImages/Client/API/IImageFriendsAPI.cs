﻿using Contracts.Dto;
using Refit;

namespace TaskUserImages.Client.API
{
    public interface IImageFriendsAPI
    {
        [Get("/user/profiles")]
        Task<List<UserDto>> GetUserProfiles(int page);
        [Post("/images/upload")]
        Task UploadImage(UploadedFile file);
        [Get("/images/img")]
        Task<object> GetImage(Guid id);
        [Get("/images/userimages")]
        Task<List<string>> GetUserImageUrls(string userName);
        [Get("/images/myimages")]
        Task<List<string>> GetMyImageUrls();
        [Get("/friends/isfriend")]
        Task<bool> IsFriend(string userName);
        [Post("/friends/add")]
        Task AddFriend(string userName);        
        [Get("/friends/requestexists")]
        Task<bool> IsRequestPending(string userName);        
        [Get("/friends/incomingrequests")]
        Task<List<FriendRequestDto>> GetIncomingFriendRequests(int page);
    }
}
