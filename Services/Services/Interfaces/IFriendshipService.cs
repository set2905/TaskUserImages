using Ardalis.Result;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IFriendshipService
    {
        /// <summary>
        /// Checks if there is a pending friend request from user with <paramref name="fromId"/> to user with <paramref name="toId"/>
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <returns></returns>
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(UserId fromId, UserId toId);
        /// <summary>
        /// Checks if there is a pending friend request from user with ASP <paramref name="fromIdentity"/> to user with <paramref name="toName"/> username
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <returns></returns>
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(string fromIdentity, string toName);
        /// <summary>
        /// Gets user friends
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<List<User>>> GetFriends(UserId userId);
        /// <summary>
        /// Gets page of incoming friend requests
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(UserId userId, int skip, int take);
        /// <summary>
        /// Gets page of incoming friend requests for user with ASP <paramref name="identityId"/>
        /// </summary>
        /// <param name="identityId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(string identityId, int skip, int take);
        /// <summary>
        /// Checks if friendship exists between user with ASP  <paramref name="userIdentity"/> and user with <paramref name="friendUsername"/>
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="friendUsername"></param>
        /// <returns></returns>
        Task<Result<bool>> IsInFriendlist(string userIdentity, string friendUsername);
        /// <summary>
        /// Sends friend request
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task<Result> SendFriendRequest(UserId from, UserId to);
        /// <summary>
        /// Sends friend request from user with ASP  <paramref name="fromIdentity"/> to user with <paramref name="toUserName"/>
        /// </summary>
        /// <param name="fromIdentity"></param>
        /// <param name="toUserName"></param>
        /// <returns></returns>
        Task<Result> SendFriendRequest(string fromIdentity, string toUserName);
    }
}