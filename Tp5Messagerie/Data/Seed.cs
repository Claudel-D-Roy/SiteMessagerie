using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tp5Messagerie.Entities;

namespace Tp5Messagerie.Data
{
    public static class SeedExtension
    {
        public static readonly PasswordHasher<ApplicationUser> PASSWORD_HASHER = new();

        public static void Seed(this ModelBuilder builder)
        {
            var adminRole = AddRole(builder, "Administrator");
            var UserNorm = AddRole(builder, "Utilisateur");
            var Admin = AddUser(builder, "Admin", "Admin123!");
            addUserToRole(builder, Admin, adminRole);
            var User1 = AddUser(builder, "UserNormie1", "User123!");
            var User2 = AddUser(builder, "UserNormie2", "User123!");
            addUserToRole(builder, User1, UserNorm);
            addUserToRole(builder, User2, UserNorm);

        }

        private static void addUserToRole(ModelBuilder builder, ApplicationUser User, IdentityRole<Guid> adminRole)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = User.Id,
                RoleId = adminRole.Id,
            });
        }

        private static IdentityRole<Guid> AddRole(ModelBuilder builder, string name)
        {
            var newRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = name,
                NormalizedName = name.ToUpper()
            };
            builder.Entity<IdentityRole<Guid>>().HasData(newRole);

            return newRole;
        }

       


        private static ApplicationUser AddUser(ModelBuilder builder,
            string userName, string password)
        {
            var newUser = new ApplicationUser(userName)
            {
                Id = Guid.NewGuid(),
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newUser.PasswordHash = PASSWORD_HASHER.HashPassword(newUser, password);
            builder.Entity<ApplicationUser>().HasData(newUser);

            return newUser;
        }
    }
}
