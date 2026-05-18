using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, Abstracts.IInstructorRepository
    {
        #region Fields
        private DbSet<Instructor> instructors;
        #endregion 

        #region Constructors
        public InstructorRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            instructors = dbcontext.Set<Instructor>();
        }
        #endregion

        #region Methods

        #endregion

    }
}
