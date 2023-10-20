using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Contracts.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/[controller]")]
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
        [Route("GetUsers")]
        public async Task<Result<List<UserDto>>> GetUsers(int page, int pageSize)
        {
            return (await userService.GetUsers(page, pageSize)).Map(uli => uli.ConvertAll(u=>mapper.Map<UserDto>(u)));
        }

    }
}
