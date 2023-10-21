using Ardalis.Result;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        Task<Result> UploadImage();
    }
}