using Microsoft.EntityFrameworkCore;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.DAL.Data;
using RouteC41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL.Repositries
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext) 
        {

        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
          return  _context.Employees.Where(E=> E.Address.ToLower() == address.ToLower());   
        }

        public IQueryable<Employee> SearchByName(string name)
        
           => _context.Employees.Where(E=>E.Name.ToLower().Contains(name));
        
    }
}
