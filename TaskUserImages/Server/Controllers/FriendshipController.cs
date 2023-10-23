using Ardalis.Result.AspNetCore;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Services.Interfaces;
using System.Security.Claims;
using AutoMapper;
using Contracts.Dto;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private const int PAGESIZE_DEFAULT = 10;
        private const string USERNOTFOUND_ERROR = "Current user not found";
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
            if (userId == null) return Result.Error(USERNOTFOUND_ERROR);
            return await friendshipService.IsInFriendlist(userId, username);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpPost]
        [Route("add")]
        public async Task<Result> SendFriendRequest(string username)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error(USERNOTFOUND_ERROR);
            return await friendshipService.SendFriendRequest(userId, username);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("requestexists")]
        public async Task<Result<bool>> IsFriendRequestPending(string username)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error(USERNOTFOUND_ERROR);
            return await friendshipService.CheckForPendingFriendshipRequestAsync(userId, username);
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("incomingrequests")]
        public async Task<Result<List<FriendRequestDto>>> GetIncomingRequests(int page)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Result.Error(USERNOTFOUND_ERROR);
            return (await friendshipService.GetIncomingFriendshipRequests(userId, (page-1)*PAGESIZE_DEFAULT, PAGESIZE_DEFAULT))
                                            .Map(frli => frli.ConvertAll(fr => mapper.Map<FriendRequestDto>(fr)));
        }
    }
}
