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
	public class DepartmentRepostry :GenericRepository<Department>, IDepartmentReposotry
	{
		public DepartmentRepostry(ApplicationDbContext dbContext):base(dbContext) 
		{

		}
	}
}
