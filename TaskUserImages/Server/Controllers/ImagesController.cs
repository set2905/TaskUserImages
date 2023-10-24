using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Contracts.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService=imageService;
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpPost]
        [Route("upload")]
        public async Task<Result> UploadImage(UploadedFile uploaded)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error("User id could not be found");

            return await imageService.UploadImage(userId, uploaded);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("userimages")]
        public async Task<Result<List<string>>> GetUserImageUrls(string userName)
        {
            string? identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (identityId == null) return Result.Error("Your user id could not be found");
            Result<List<(ImageId imgId, string key)>> result = await imageService.GetUserImageUrlsQueryData(userName, identityId);
            if (!result.IsSuccess) return Result.Forbidden();
            List<string> converted = result.Value.ConvertAll(x => Url.Action("GetImage",
                                                                             "Images",
                                                                             new { imageId = x.imgId.Value, key = x.key },
                                                                             protocol: Request.Scheme)??"");
            return Result.Success(converted);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("myimages")]
        public async Task<Result<List<string>>> GetMyImageUrls()
        {
            string? identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (identityId == null) return Result.Error("Your user id could not be found");
            Result<List<(ImageId imgId, string key)>> result = await imageService.GetUserImageUrlsQueryData(identityId);
            if (!result.IsSuccess) return Result.Forbidden();
            List<string> converted = result.Value.ConvertAll(x => Url.Action("GetImage",
                                                                             "Images",
                                                                             new { imageId = x.imgId.Value, key = x.key },
                                                                             protocol: Request.Scheme)??"");
            return Result.Success(converted);
        }


        [HttpGet]
        [Route("img")]
        public async Task<IActionResult> GetImage(Guid imageId, string key)
        {
            string contentType = "image/jpeg";
            Result<string> pathResult = await imageService.GetImageFilePath(new(imageId), key);
            if (!pathResult.IsSuccess) return Forbid();
            return PhysicalFile(pathResult.Value, contentType);
        }

    }
}
