using Microsoft.AspNetCore.Mvc;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;

namespace RouteC41.G02.PL.Controllers
{
	//Inheritance : Department Controller is a Controller
	//Association :Department Controller Has a DepartmentRepostry
	public class DepartmentController : Controller
	{
		private readonly IDepartmentReposotry _repostry;

		public DepartmentController(IDepartmentReposotry repostry)
        {
			_repostry = repostry;
			_repostry = repostry;
		}

        // /Department/Index
        public IActionResult Index()
		{
			return View();
		}
	}
}
