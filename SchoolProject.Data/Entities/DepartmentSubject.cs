using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject
    {
        public int DID { get; set; }
        public int SubId { get; set; }

        [ForeignKey("DID")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubId")]
        public virtual Subjects? Subject { get; set; }
    }
}
