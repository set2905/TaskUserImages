using Ardalis.Result.AspNetCore;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Services.Interfaces;
using System.Security.Claims;
using AutoMapper;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService friendshipService;
        private readonly IMapper mapper;


        public FriendshipController(IFriendshipService friendshipService, IMapper mapper)
        {
            this.friendshipService=friendshipService;
            this.mapper=mapper;
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("isfriend")]
        public async Task<Result<bool>> IsFriend(string username)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error("Current user not found");
            return await friendshipService.IsInFriendlist(userId, username);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpPost]
        [Route("add")]
        public async Task<Result> SendFriendRequest(string username)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error("Current user not found");
            return await friendshipService.SendFriendRequest(userId, username);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("requestexists")]
        public async Task<Result<bool>> IsFriendRequestPending(string username)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error("Current user not found");
            return await friendshipService.CheckForPendingFriendshipRequestAsync(userId, username);
        }
    }
}
