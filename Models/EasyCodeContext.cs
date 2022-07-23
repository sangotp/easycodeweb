using Microsoft.EntityFrameworkCore;

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
        }

        // Table
        public DbSet<Category> categories { get; set; }


    }
}
