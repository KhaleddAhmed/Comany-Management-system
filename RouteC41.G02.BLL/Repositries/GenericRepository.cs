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
        public  void Add(T entity)
          =>  _context.Set<T>().Add(entity);


           

        
        public void Update(T entity)
          => _context.Set<T>().Update(entity);

        
        public void Delete(T entity)
           => _context.Set<T>().Remove(entity);
    
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
                return (IEnumerable<T>) await _context.Employees.Include(E=>E.Department).AsNoTracking().ToListAsync();
            else
                return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            ///var Employee=_context.Employees.Local.Where(D=>D.Id==id).FirstOrDefault();
            ///if (Employee==null)
            ///	 Employee = _context.Employees.Where(D => D.Id == id).FirstOrDefault();
            ///return Employee;

            return await _context.Set<T>().FindAsync(id);

        }
    }
}
