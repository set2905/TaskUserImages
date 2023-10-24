using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRequestRepository friendshipRequestRepository;
        private readonly IUserProfileRepository userProfileRepository;

        public FriendshipService(IFriendshipRequestRepository friendshipRequestRepository, IUserProfileRepository userProfileRepository)
        {
            this.friendshipRequestRepository=friendshipRequestRepository;
            this.userProfileRepository=userProfileRepository;
        }

        public async Task<Result<bool>> CheckForPendingFriendshipRequestAsync(UserId from, UserId to)
        {
            return await friendshipRequestRepository.CheckForPendingFriendshipRequestAsync(from, to);
        }

        public async Task<Result<bool>> CheckForPendingFriendshipRequestAsync(string fromIdentity, string toName)
        {
            var userResult = await userProfileRepository.GetByIdentityAsync(fromIdentity);
            var friendResult = await userProfileRepository.GetByUserNameAsync(toName);
            if (!userResult.IsSuccess||!friendResult.IsSuccess) return Result.NotFound("User not found");
            return await friendshipRequestRepository.CheckForPendingFriendshipRequestAsync(userResult.Value.Id, friendResult.Value.Id);
        }

        public async Task<Result<bool>> IsInFriendlist(string userIdentity, string friendName)
        {
            var userResult = await userProfileRepository.GetByIdentityAsync(userIdentity);
            var friendResult = await userProfileRepository.GetByUserNameAsync(friendName);
            if (!userResult.IsSuccess||!friendResult.IsSuccess) return Result.NotFound("User not found");
            return await userProfileRepository.IsInFriendlist(userResult.Value.Id, friendResult.Value.Id);
        }

        public async Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(UserId userId, int skip, int take)
        {
            return await friendshipRequestRepository.GetIncomingFriendshipRequests(userId, skip, take);
        }

        public async Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(string identityId, int skip, int take)
        {
            var userResult = await userProfileRepository.GetByIdentityAsync(identityId);
            if (!userResult.IsSuccess) return Result.NotFound("User not found");
            return await GetIncomingFriendshipRequests(userResult.Value.Id, skip, take);
        }

        public async Task<Result<List<User>>> GetFriends(UserId userId)
        {
            return await userProfileRepository.GetFriends(userId);
        }

        public async Task<Result> SendFriendRequest(UserId from, UserId to)
        {
            if(from==to) return Result.Conflict("Cant send friend request to yourself");
            Result<User> fromUserResult = await userProfileRepository.GetByIdAsync(from);
            Result<User> toUserResult = await userProfileRepository.GetByIdAsync(to);
            if (!fromUserResult.IsSuccess||!toUserResult.IsSuccess) return Result.NotFound("User not found");

            Result<FriendshipRequest> incomingRequestResult = await friendshipRequestRepository.FindFriendRequestAsync(toUserResult.Value.Id,
                                                                                           fromUserResult.Value.Id);
            if (incomingRequestResult.IsSuccess)
            {
                return await incomingRequestResult.Value.Accept(friendshipRequestRepository);
            }

            FriendshipRequest friendshipRequest = new(new(Guid.NewGuid()), fromUserResult.Value, toUserResult.Value);
            return await friendshipRequestRepository.InsertAsync(friendshipRequest);
        }

        public async Task<Result> SendFriendRequest(string fromIdentity, string toUserName)
        {
            var userResult = await userProfileRepository.GetByIdentityAsync(fromIdentity);
            var friendResult = await userProfileRepository.GetByUserNameAsync(toUserName);
            if (!userResult.IsSuccess||!friendResult.IsSuccess) return Result.NotFound("User not found");
            return await SendFriendRequest(userResult.Value.Id, friendResult.Value.Id);
        }
    }
}
