using Microsoft.AspNetCore.Identity;

namespace Tp5Messagerie.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser(string userName) :
            base(userName)
        { }

        public List<Message>? MessUser { get; } = new();
    }
}
