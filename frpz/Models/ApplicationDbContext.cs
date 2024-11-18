using Microsoft.EntityFrameworkCore;

namespace frpz.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=26.205.250.8;Database=frpz;Username=stas;Password=stas2002");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTestResult>()
                .HasOne(utr => utr.User)
                .WithMany() 
                .HasForeignKey(utr => utr.UserId);

            modelBuilder.Entity<UserTestResult>()
                .HasOne(utr => utr.Test)
                .WithMany() 
                .HasForeignKey(utr => utr.TestId);
        }
    }
}