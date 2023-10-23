using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class FriendRequestDto
    {
        public Guid FromUserId { get; set; }
        public Guid Id { get; set; }
    }
}
