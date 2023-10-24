using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Contracts.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const int PAGESIZE_DEFAULT = 10;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService=userService;
            this.mapper=mapper;
        }

        [Authorize]
        [TranslateResultToActionResult]
        [HttpGet]
        [Route("profiles")]
        public async Task<Result<List<UserDto>>> GetUsers(int page)
        {
            return (await userService.GetUserProfiles(page, PAGESIZE_DEFAULT)).Map(uli => uli.ConvertAll(u=>mapper.Map<UserDto>(u)));
        }

    }
}
