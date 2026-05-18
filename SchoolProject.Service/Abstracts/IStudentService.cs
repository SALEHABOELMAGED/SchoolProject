using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public IQueryable<Student> GetStudentsAsQueryable();
        public IQueryable<Student> GetStudentsByDepartmentIdAsQueryable(int DID);
        public IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderingEnum, string search);
    }
}
