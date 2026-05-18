namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Data.Entities.Department, Features.Department.Queries.Results.GetDepartmentByIdResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.Name : null))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.StudentList, opt => opt.Ignore())
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<Data.Entities.DepartmentSubject, Features.Department.Queries.Results.SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.SubjectName));

            CreateMap<Data.Entities.Student, Features.Department.Queries.Results.StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Data.Entities.Instructor, Features.Department.Queries.Results.InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
