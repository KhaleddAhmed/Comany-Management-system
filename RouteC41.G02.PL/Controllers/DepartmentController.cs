using Microsoft.AspNetCore.Mvc;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Models;
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
			var departments = _repostry.GetAll();
			return View(departments);
		}


		[HttpGet]
		public IActionResult Create()
		{
			
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{
			if(ModelState.IsValid)//Server side validation
			{
				var count=_repostry.Add(department);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			return View(department);

		}
	}
}
