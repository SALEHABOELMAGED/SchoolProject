using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, Abstracts.IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> _departments;
        #endregion 

        #region Constructors
        public DepartmentRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _departments = dbcontext.Set<Department>();
        }
        #endregion

        #region Methods

        #endregion

    }
}
