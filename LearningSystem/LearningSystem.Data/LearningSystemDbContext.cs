namespace LearningSystem.Data
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Article> Articles { get; set; }

        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCourse>()
                .ToTable("UsersCourses")
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);

            builder.Entity<User>()
                .HasMany(u => u.Articles)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);


            base.OnModelCreating(builder);
        }
    }
}
