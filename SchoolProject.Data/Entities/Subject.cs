using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data.Entities
{
    public class Subjects
    {
        public Subjects()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmentsSubjects = new HashSet<DepartmentSubject>();
            InstructorsSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        public int SubId { get; set; }
        [StringLength(500)]
        public string? SubjectName { get; set; }
        public int? Period { get; set; }

        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }
        public virtual ICollection<InstructorSubject> InstructorsSubjects { get; set; }
    }
}
