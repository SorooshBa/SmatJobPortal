using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmatJobPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApply> JobApply { get; set; }
        public DbSet<ProfileView> ProfileViews { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // رابطه JobApply -> Job
            builder.Entity<JobApply>()
                .HasOne(ja => ja.Job)
                .WithMany(j => j.JobApplies)
                .HasForeignKey(ja => ja.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // رابطه JobApply -> ApplicationUser
            builder.Entity<JobApply>()
                .HasOne(ja => ja.User)
                .WithMany(u => u.JobApplies)
                .HasForeignKey(ja => ja.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique profile view per viewer per viewed user
            builder.Entity<ProfileView>()
                .HasIndex(p => new { p.ViewedUserId, p.ViewerUserId })
                .IsUnique();
        }
    }
}

