using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Tp5Messagerie.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Tp5Messagerie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
            .HasMany(mess => mess.Commentaires)
            .WithOne(com => com.Messages)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Messages)
                .WithMany(m => m.Commentaires)
                 .HasForeignKey(c => c.MessageID)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.MessUser)
                .WithOne(mess => mess.UserCom)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Seed();
        }

    }
}
