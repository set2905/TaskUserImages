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
            UserId=userId;
            FriendId=friendId;
        }

        public UserId UserId { get; private set; }
        public UserId FriendId { get; private set; }
        
    }
}
