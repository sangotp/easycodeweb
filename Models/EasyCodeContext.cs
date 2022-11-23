using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Models
{
    public class EasyCodeContext : IdentityDbContext<AppUser>
    {
        public EasyCodeContext(DbContextOptions<EasyCodeContext> options) : base(options)
        {
            //..
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Course - CourseDetails (1 - 1)
            modelBuilder.Entity<CourseDetails>(entity =>
            {
                entity.HasOne(d => d.Course)
                .WithOne(d => d.CourseDetails)
                .HasForeignKey<CourseDetails>(c => c.CourseDetailsId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // Remove Prefix "AspNet" For Identity Tables
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if(tableName is not null)
                {
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }
            }
        }

        // Table
        public DbSet<Category> categories { get; set; }

        public DbSet<Topic> topics { get; set; }

        public DbSet<Course> courses { get; set; }

        public DbSet<CourseDetails> courseDetails { get; set; }

        public DbSet<CourseChapter> courseChapters { get; set; }

        public DbSet<CourseLesson> courseLessons { get; set; }

        public DbSet<CourseExerise> courseExerises { get; set; }

        public DbSet<ECAPayment> ECAPayments { get; set; }
    }
}
