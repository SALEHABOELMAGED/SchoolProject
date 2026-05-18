using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {

            // Connection to SQL Server database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("dbcontext")));

            return services;
        }
    }
}
