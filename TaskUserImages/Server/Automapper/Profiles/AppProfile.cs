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
                .ForMember(x => x.Id, opt => opt.MapFrom<FriendRequestIdValueResolver>());
        }
    }

    public class FriendRequestIdValueResolver : IValueResolver<FriendshipRequest, FriendRequestDto, Guid>
    {
        public Guid Resolve(FriendshipRequest source, FriendRequestDto destination, Guid destMember, ResolutionContext context)
        {
            return source.Id.Value;
        }
    }

}
