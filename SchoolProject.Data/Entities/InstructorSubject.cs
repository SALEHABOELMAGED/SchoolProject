using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        public int InstructorId { get; set; }
        public int SubjectId { get; set; }

        [ForeignKey("InstructorId")]
        public virtual Instructor? Instructor { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subjects? Subject { get; set; }
    }
}
