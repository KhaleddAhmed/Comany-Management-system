using Microsoft.Extensions.DependencyInjection;
using RouteC41.G02.BLL;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;

namespace RouteC41.G02.PL.Extenstions
{
    public static class ApplicationServiceExtenstions
    {
        public static void AddApplicationServies(this IServiceCollection services)
        {
            //services.AddTransient<IDepartmentReposotry, DepartmentRepostry>();
            //services.AddScoped<IDepartmentReposotry, DepartmentRepostry>();
            ////services.AddSingleton<IDepartmentReposotry, DepartmentRepostry>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
