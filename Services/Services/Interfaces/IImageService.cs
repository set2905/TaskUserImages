using Ardalis.Result;
using Contracts.Dto;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        Task<Result<string>> GetImageFilePath(ImageId id, string key);
        Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string otherUserName, string myIdentityId);
        Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string myIdentityId);
        Task<Result> UploadImage(string userId, UploadedFile imageToUpload);
    }
}