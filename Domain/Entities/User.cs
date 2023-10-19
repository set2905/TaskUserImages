using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class User
    {
        private readonly HashSet<Image> images = new();
        private User() { }

        public UserId Id { get; private set; }
        public IReadOnlyList<Image> Images => images.ToList();

    }
    public record UserId(Guid Value);
}
