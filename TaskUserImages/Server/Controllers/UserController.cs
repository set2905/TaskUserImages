using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Contracts.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService=userService;
            this.mapper=mapper;
        }

        [TranslateResultToActionResult]
        [HttpGet]
        [Route("profiles")]
        public async Task<Result<List<UserDto>>> GetUsers(int page, int pageSize)
        {
            return (await userService.GetUserProfiles(page, pageSize)).Map(uli => uli.ConvertAll(u=>mapper.Map<UserDto>(u)));
        }

    }
}
