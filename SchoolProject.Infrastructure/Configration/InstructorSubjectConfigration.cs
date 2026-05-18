using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class InstructorSubjectConfigration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InstructorSubject> builder)
        {
            // InstructorSubject composite PK
            builder
                .HasKey(x => new { x.InstructorId, x.SubjectId });
            builder
                .HasOne(x => x.Instructor)
                .WithMany(i => i.InstructorsSubjects)
                .HasForeignKey(x => x.InstructorId);
            builder
                .HasOne(x => x.Subject)
                .WithMany(s => s.InstructorsSubjects)
                .HasForeignKey(x => x.SubjectId);
        }
    }
}