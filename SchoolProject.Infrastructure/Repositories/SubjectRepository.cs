using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, Abstracts.ISubjectRepository
    {
        #region Fields
        private DbSet<Subjects> subjects;
        #endregion 

        #region Constructors
        public SubjectRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            subjects = dbcontext.Set<Subjects>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
