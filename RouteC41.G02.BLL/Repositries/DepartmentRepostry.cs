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
	public class DepartmentRepostry : IDepartmentReposotry
	{
		private readonly ApplicationDbContext _context;

        public DepartmentRepostry(ApplicationDbContext dbContext)
        {
			_context = dbContext;
        }
        public int Add(Department entity)
		{
			_context.Departments.Add(entity);
			return _context.SaveChanges();

		}
		public int Update(Department entity)
		{
			_context.Departments.Update(entity);
			return _context.SaveChanges();
			
		}
		public int Delete(Department entity)
		{
			_context.Departments.Remove(entity);
			return _context.SaveChanges();
		}

		public IEnumerable<Department> GetAll()
		{ 
			return _context.Departments.AsNoTracking().ToList();
		}

		public Department GetById(int id)
		{
			///var department=_context.Departments.Local.Where(D=>D.Id==id).FirstOrDefault();
			///if (department==null)
			///	 department = _context.Departments.Where(D => D.Id == id).FirstOrDefault();
			///return department;

			return _context.Departments.Find(id);
			
		}

	
	}
}
