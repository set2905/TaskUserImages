using AutoMapper;
using Contracts.Dto;
using Domain.Entities;

namespace TaskUserImages.Server.Automapper.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
