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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();

        }
        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();

        }
        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
                return (IEnumerable<T>) _context.Employees.Include(E=>E.Department).AsNoTracking();
            else
                return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            ///var Employee=_context.Employees.Local.Where(D=>D.Id==id).FirstOrDefault();
            ///if (Employee==null)
            ///	 Employee = _context.Employees.Where(D => D.Id == id).FirstOrDefault();
            ///return Employee;

            return _context.Set<T>().Find(id);

        }
    }
}
