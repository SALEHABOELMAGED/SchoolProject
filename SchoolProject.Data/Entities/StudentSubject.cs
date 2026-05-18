using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        public int StudId { get; set; }
        public int SubId { get; set; }
        public decimal? Grade { get; set; }

        [ForeignKey("StudId")]
        public virtual Student? Students { get; set; }

        [ForeignKey("SubId")]
        public virtual Subjects? Subjects { get; set; }
    }
}
