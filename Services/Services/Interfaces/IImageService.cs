using Ardalis.Result;
using Contracts.Dto;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        Task<Result> UploadImage(string userId, UploadedFile imageToUpload);
    }
}