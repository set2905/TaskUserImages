using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Contracts.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService=imageService;
        }

        [TranslateResultToActionResult]
        [HttpPost]
        [Route("Upload")]
        public async Task<Result> UploadImage(UploadedFile uploaded)
        {
            var path = $"C:\\Users\\user\\Downloads\\{Guid.NewGuid()}";
            using (FileStream fs = System.IO.File.Create(path))
            {
                await fs.WriteAsync(uploaded.FileContent, 0, uploaded.FileContent.Length);
                fs.Close();
            }
            return Result.Success();
        }

    }
}
