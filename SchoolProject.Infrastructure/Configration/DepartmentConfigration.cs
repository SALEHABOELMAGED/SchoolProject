using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class DepartmentConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            // Department <-> Instructor (manager)
            builder
                .HasOne(d => d.Instructor)
                .WithOne(i => i.DepartmentManager)
                .HasForeignKey<Department>(d => d.InsManager)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
