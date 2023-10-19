using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public class User
    {
        private readonly HashSet<Image> images = new();

        public UserId Id { get; private set; }
        public IReadOnlyList<Image> Images=>images.ToList();

    }
    public record UserId(Guid Value);
}
