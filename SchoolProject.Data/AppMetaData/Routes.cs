namespace SchoolProject.Data.AppMetaData
{
    public static class Routes
    {
        public const string Root = "Api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class StudentRouting
        {
            public const string Prefix = Base + "/Student";
            public const string GetStudentsList = Prefix + "/List";
            public const string GetStudentById = Prefix + "/{id}";
            public const string Create = Prefix + "/Create";
            public const string Update = Prefix + "/Update";
            public const string Delete = Prefix + "/Delete/{id}";
            public const string Paginated = Prefix + "/Paginated";
        }
        public static class DepartmentRouting
        {
            public const string Prefix = Base + "/Department";
            public const string GetDepartmentById = Prefix + "/Id";

        }
        public static class AppUserRouting
        {
            public const string Prefix = Base + "/User";
            public const string AddUser = Prefix + "/Add";

        }
    }
}
