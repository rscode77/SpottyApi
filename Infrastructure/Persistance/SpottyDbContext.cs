using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class SpottyDbContext : DbContext
    {
        public SpottyDbContext(DbContextOptions<SpottyDbContext> options)
            : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventLogs> EventLogs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (
                var relationship in modelBuilder
                    .Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys())
            )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);

            modelBuilder
                .Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithOne(u => u.UserActivity)
                .HasForeignKey<UserActivity>(ua => ua.UserId);

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.UserProfiles)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfiles>(up => up.UserId);

            //modelBuilder.Entity<User>(r =>
            //{
            //    r.Property(u => u.Username).IsRequired();
            //    r.Property(u => u.Email).IsRequired();
            //    r.Property(u => u.Password).IsRequired();
            //});
        }
    }
}