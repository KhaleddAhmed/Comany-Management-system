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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public int Add(Employee entity)
        {
            _context.Employees.Add(entity);
            return _context.SaveChanges();

        }
        public int Update(Employee entity)
        {
            _context.Employees.Update(entity);
            return _context.SaveChanges();

        }
        public int Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.AsNoTracking().ToList();
        }

        public Employee GetById(int id)
        {
            ///var Employee=_context.Employees.Local.Where(D=>D.Id==id).FirstOrDefault();
            ///if (Employee==null)
            ///	 Employee = _context.Employees.Where(D => D.Id == id).FirstOrDefault();
            ///return Employee;

            return _context.Employees.Find(id);

        }
    }
}
