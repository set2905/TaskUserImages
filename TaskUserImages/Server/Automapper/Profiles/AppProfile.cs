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
            CreateMap<FriendshipRequest, FriendRequestDto>()
                .ForMember(x => nameof(x.Id), opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(x => nameof(x.FromUserId), opt => opt.MapFrom(src => src.UserId.Value));
        }
    }
}
