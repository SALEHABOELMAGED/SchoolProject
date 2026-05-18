using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentrepository;
        #endregion
        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentrepository = studentRepository;
        }
        #endregion
        #region Handle Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentrepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = _studentrepository.GetTableNoTracking()
                                            .Include(x => x.Department)
                                            .Where(x => x.StudentId == id)
                                            .FirstOrDefaultAsync();
            return await student;
        }

        public async Task<string> AddAsync(Student student)
        {

            await _studentrepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExist(string name)
        {
            return await _studentrepository.GetTableNoTracking().AnyAsync(x => x.Name == name);// AnyAsync returns true if any record matches, false if none
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            return await _studentrepository.GetTableNoTracking().AnyAsync(x => x.Name == name && x.StudentId != id);
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentrepository.UpdateAsync(student);
            return "Success";
        }
        public async Task<string> DeleteAsync(Student student)
        {
            var transaction = _studentrepository.BeginTransaction();
            try
            {
                await _studentrepository.DeleteAsync(student);
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return $"Error: {ex.Message}";
            }


        }

        public IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderingEnum, string? search)
        {
            var querable = _studentrepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Name.Contains(search) || x.Address.Contains(search));

            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.Name);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DName);
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudentId);
                    break;
            }
            return querable;
        }

        public IQueryable<Student> GetStudentsAsQueryable()
        {
            return _studentrepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> GetStudentsByDepartmentIdAsQueryable(int DID)
        {
            return _studentrepository.GetTableNoTracking().Where(x => x.DID == DID).AsQueryable();
        }
        #endregion
    }
}
