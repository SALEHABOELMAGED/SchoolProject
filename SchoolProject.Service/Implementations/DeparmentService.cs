using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion


        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region Handle Functions
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var student = await _departmentRepository.GetTableNoTracking().Where(x => x.DID == id)
                                                         .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                         .Include(x => x.Instructors)
                                                         .Include(x => x.Instructor)
                                                         .FirstOrDefaultAsync();
            return student;
        }

        public async Task<bool> IsDepartmentIdExist(int? departmentId)
        {
            if (departmentId == null)
                return false;

            return await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DID == departmentId);
        }
        #endregion

    }
}
