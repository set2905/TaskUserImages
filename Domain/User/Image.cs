using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public class Image
    {
        public ImageId Id { get; private set; }
        public UserId UserId { get; private set; }
        public string FileName { get; private set; }
    }
    public record ImageId(Guid Value);
}
