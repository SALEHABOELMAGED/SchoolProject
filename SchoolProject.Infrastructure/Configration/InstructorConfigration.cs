using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class InstructorConfigration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Instructor> builder)
        {
            // Instructor Salary precision
            builder
                .Property(i => i.Salary)
                .HasPrecision(18, 2);

            // Instructor self-referencing (supervisor)
            builder
                .HasOne(i => i.Supervisor)
                .WithMany(i => i.Instructors)
                .HasForeignKey(i => i.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}