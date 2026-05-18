using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class StudentSubjectConfigration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudentSubject> builder)
        {
            // StudentSubject composite PK
            builder
                .HasKey(ss => new { ss.StudId, ss.SubId });
            builder
                .HasOne(ss => ss.Students)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudId);
            builder
                .HasOne(ss => ss.Subjects)
                .WithMany(s => s.StudentsSubjects)
                .HasForeignKey(ss => ss.SubId);

        }
    }
}
