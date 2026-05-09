// Models for the database context

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Models for the database context
namespace CoblentzContext.Models
{
    // Database context
    public class CoblentzContext : IdentityDbContext<User>
    {
        // Constructor
        public CoblentzContext (DbContextOptions<CoblentzContext> options)
            : base(options) 
        {}

        // Database tables
        public DbSet<Song> Song { get; set; } = null!;
        public DbSet<Favorite> Favorites { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;

        // Model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Base model creating
            base.OnModelCreating(modelBuilder);

            // Configure User ID length
            modelBuilder.Entity<User>(entity => {
                entity.Property(m => m.Id).HasMaxLength(128);
            });

            // Configure Favorite primary key
            modelBuilder.Entity<Favorite>().HasKey(f => new { f.UserId, f.SongId });

            // Configure Favorite relationships
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Song)
                .WithMany()
                .HasForeignKey(f => f.SongId)
                .OnDelete(DeleteBehavior.Restrict);

            // IDs for Seeding
            string adminRoleId = "admin-role-id";
            string adminUserId = "admin-user-id";

            // 1. Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            // 2. Seed Admin User (Startup logic in Program.cs handles the password)
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-SECURITY-STAMP-ADMIN",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-ADMIN"
            });

            // 3. Assign Admin User to Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

            // 4. Seed Projects
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Name = "The Jones Experiment" },
                new Project { ProjectId = 2, Name = "Fidget" },
                new Project { ProjectId = 3, Name = "Adam Knight's Buried Alive" }
            );

            // 5. Seed Songs with ProjectId
            modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    SongId = 1,
                    Title = "Mi Amigo",
                    Artist = "Kings of Leon",
                    ProjectId = 1,
                    Status = "Learning",
                    UserId = adminUserId
                },
                new Song {
                    SongId = 2,
                    Title = "I Wanna Be Your Lover",
                    Artist = "Prince",
                    ProjectId = 2,
                    Status = "Ready",
                    UserId = adminUserId
                },
                new Song
                {
                    SongId = 3,
                    Title = "The Final Hurrah",
                    Artist = "Phish",
                    ProjectId = 3,
                    Status = "Ready",
                    UserId = adminUserId
                }, new Song
                {
                    SongId = 4,
                    Title = "Clint Eastwood",
                    Artist = "Gorillaz",
                    ProjectId = 2,
                    Status = "Learning",
                    UserId = adminUserId
                }
            );
        }
    }
}
