using Ardalis.Result;
using Contracts.Dto;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Gets filepath of image. Requires key to gain access
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Result<string>> GetImageFilePath(ImageId id, string key);
        /// <summary>
        /// Gets list of keys and ids of images of user with <paramref name="otherUserName"/>. 
        /// User with ASP <paramref name="myIdentityId"/> is required to have access to images of user with <paramref name="otherUserName"/>
        /// </summary>
        /// <param name="otherUserName"></param>
        /// <param name="myIdentityId"></param>
        /// <returns></returns>
        Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string otherUserName, string myIdentityId);
        /// <summary>
        /// Gets list of keys and ids of images of user with ASP <paramref name="myIdentityId"/>
        /// </summary>
        /// <param name="myIdentityId"></param>
        /// <returns></returns>
        Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string myIdentityId);
        /// <summary>
        /// Uploads an image and assigns it to the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageToUpload"></param>
        /// <returns></returns>
        Task<Result> UploadImage(string userId, UploadedFile imageToUpload);
    }
}