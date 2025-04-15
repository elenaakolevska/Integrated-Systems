using Courses.Domain.DomainModels;
using Courses.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<CoursesApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }

    }
}
