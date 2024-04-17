using RouteC41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);

        IQueryable<Employee> SearchByName(string name);
    }
}
