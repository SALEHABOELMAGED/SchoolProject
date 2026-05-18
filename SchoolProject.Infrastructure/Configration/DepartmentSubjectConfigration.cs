using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class DepartmentSubjectConfigration : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DepartmentSubject> builder)
        {
            // DepartmentSubject composite PK
            builder
                .HasKey(ds => new { ds.DID, ds.SubId });
            builder
                .HasOne(ds => ds.Department)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DID);
            builder
                .HasOne(ds => ds.Subject)
                .WithMany(s => s.DepartmentsSubjects)
                .HasForeignKey(ds => ds.SubId);
        }
    }
}