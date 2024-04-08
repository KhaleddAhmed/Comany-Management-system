using RouteC41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL.Interfaces
{
	public interface IDepartmentReposotry
	{
		IEnumerable<Department> GetAll();

		Department GetById(int id);

		int Add(Department entity);

		int Update(Department entity);

		int Delete(Department entity);


	}
}
