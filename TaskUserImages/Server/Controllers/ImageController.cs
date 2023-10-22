using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Contracts.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using System.Security.Claims;

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

        [Authorize]
        [TranslateResultToActionResult]
        [HttpPost]
        [Route("Upload")]
        public async Task<Result> UploadImage(UploadedFile uploaded)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error("User id could not be found");

            return await imageService.UploadImage(userId, uploaded);
        }

    }
}
