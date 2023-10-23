using Ardalis.Result;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(UserId from, UserId to);
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(string fromIdentity, string toName);
        Task<Result<List<User>>> GetFriends(UserId userId);
        Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(UserId userId, int skip, int take);
        Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(string identityId, int skip, int take);
        Task<Result<bool>> IsInFriendlist(string userIdentity, string frienUsername);
        Task<Result> SendFriendRequest(UserId from, UserId to);
        Task<Result> SendFriendRequest(string fromIdentity, string toUserName);
    }
}