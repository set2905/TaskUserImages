using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Friendship
    {
        private Friendship()
        {
        }

        public Friendship(UserId userId, UserId friendId)
        {
            Guard.Against.Default(userId.Value, nameof(userId), "The identifier is required.");
            Guard.Against.Default(friendId.Value, nameof(friendId), "The identifier is required.");
            UserId=userId;
            FriendId=friendId;
        }

        public UserId UserId { get; private set; }
        public UserId FriendId { get; private set; }
        
    }
}
