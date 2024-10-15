using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Cabinet_Prototype.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<UserRequests> UserRequests { get; set; }

        public override DbSet<User> Users { get; set; }

        public override DbSet<Role> Roles { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<CourseTeacher> CourseTeachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseTeacher>()
            .HasKey(ct => ct.Id);
            
            builder.Entity<CourseTeacher>()
            .HasOne<User>() 
            .WithMany(u => u.CourseTeachers) 
            .HasForeignKey(ct => ct.TeacherId) 
            .IsRequired(); 
        }
    }
}
