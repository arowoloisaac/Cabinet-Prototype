using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
