using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SchoolProject.Data.Entities.Identity.AppUser> Users { get; set; }
        public DbSet<SchoolProject.Data.Entities.Student> Students { get; set; }
        public DbSet<SchoolProject.Data.Entities.Department> Departments { get; set; }
        public DbSet<SchoolProject.Data.Entities.Subjects> Subjects { get; set; }
        public DbSet<SchoolProject.Data.Entities.Instructor> Instructors { get; set; }
        public DbSet<SchoolProject.Data.Entities.StudentSubject> StudentSubjects { get; set; }
        public DbSet<SchoolProject.Data.Entities.DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<SchoolProject.Data.Entities.InstructorSubject> InstructorSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
