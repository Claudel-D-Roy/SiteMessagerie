using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tp5Messagerie.Entities;

namespace Tp5Messagerie.Utilities
{
    public class DomainAsserts
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DomainAsserts(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public void Exists(object entity, string errorMessage = "The resource cannot be found.")
        {
            if (entity is null)
            {
                throw new ArgumentNullException(errorMessage);
            }
        }

        public void IsOwnedByCurrentUser(object entity, ClaimsPrincipal user,
                    string errorMessage = "You must own the resource.")
        {
            var userId = userManager.GetUserId(user);

            var ownerIdProp = entity.GetType().GetProperty("UserId");

            if (ownerIdProp is null)
            {
                throw new UnauthorizedAccessException(errorMessage);
            }

            var ownerIdValue = ownerIdProp.GetValue(entity);

            if (Guid.Equals(ownerIdValue, userId))
            {
                throw new UnauthorizedAccessException(errorMessage);
            }
        }
    }

}
