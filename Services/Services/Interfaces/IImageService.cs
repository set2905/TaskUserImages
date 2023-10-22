using Ardalis.Result;
using Contracts.Dto;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        Task<Result<string>> GetImageFilePath(ImageId id);
        Task<Result> UploadImage(string userId, UploadedFile imageToUpload);
    }
}