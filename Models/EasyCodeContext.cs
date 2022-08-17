using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Models
{
    public class EasyCodeContext : DbContext
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
        }

        // Table
        public DbSet<Category> categories { get; set; }

        public DbSet<Topic> topics { get; set; }

        public DbSet<Course> courses { get; set; }

        public DbSet<CourseDetails> courseDetails { get; set; }
    }
}
