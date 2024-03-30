using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentReposotry DepartmentRepository {  get; set; }=null;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            DepartmentRepository = new DepartmentRepostry(dbContext);
            EmployeeRepository = new EmployeeRepository(dbContext);


        }


        public int Complete()
        {
           return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();//close connection
        }
    }
}
