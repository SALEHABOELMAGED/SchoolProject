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
            public const string Paginated = Prefix + "/Paginated";
            public const string GetUserById = Prefix + "/{id}";
            public const string UpdateUser = Prefix + "/{id}/Update";
            public const string DeleteUser = Prefix + "/{id}/Delete";
            public const string ChangePassword = Prefix + "/{id}/ChangePassword";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Base + "/Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string SignUp = Prefix + "/SignUp";
        }
    }
}
