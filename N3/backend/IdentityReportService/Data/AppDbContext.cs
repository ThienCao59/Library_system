using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityReportService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityReportService.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LibraryCard> LibraryCards { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<FidoStoredCredential> FidoStoredCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Required for Identity

            // Rename Identity Tables to match requirements exactly
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.LibraryCard)
                .WithOne(l => l.User)
                .HasForeignKey<LibraryCard>(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
