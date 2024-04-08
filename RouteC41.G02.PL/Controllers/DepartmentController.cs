using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Models;
using System;
namespace RouteC41.G02.PL.Controllers
{
	//Inheritance : Department Controller is a Controller
	//Association :Department Controller Has a DepartmentRepostry
	public class DepartmentController : Controller
	{
		private readonly IDepartmentReposotry _repostry;
        private readonly IWebHostEnvironment env;

        public DepartmentController(IDepartmentReposotry departmentrepostry,IWebHostEnvironment env)
        {
			_repostry = departmentrepostry;
            this.env = env;
            _repostry = departmentrepostry;
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

		[HttpGet]
		public IActionResult Details(int? id,string ViewName="Details")
		{
			if (id is null)
				return BadRequest();
			var department = _repostry.GetById(id.Value);
			if(department == null)
				return NotFound();

			return View(ViewName,department);

		}

		// /Department/Edit/10
		[HttpGet]
		public IActionResult Edit(int? id) 
		{
			///if(!id.HasValue)
			///	return BadRequest();
			///
			///var department=_repostry.GetById(id.Value);
			///
			///if(department is null)
			///	return NotFound();
			///
			///return View(department);
			
			return Details(id,"Edit");

			
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int id,Department department)
		{


			if (id != department.Id)
				return BadRequest("An Error ya hamada");
			if (!ModelState.IsValid)
			{
				return View(department);

			}
			
			try
			{
                _repostry.Update(department);
                return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				//1.Log Exception
				//2.Friendly Messagee
				if (env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);

				else
					ModelState.AddModelError(string.Empty, "An Error Has ccured During Update The Department");

				return View (department);
			}

		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if(id is null)
				return BadRequest();
			var department=_repostry.GetById(id.Value);
			if (department == null)
				return NotFound();

			return View(department);
		}

		[HttpPost]
		public IActionResult Delete(Department department)
		{
          
            try
			{
				_repostry.Delete(department);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				//Log Exception
				//return View("Error",ex.Message);
				if (env.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, "Cannot be deeleted");
					
				}
				
                    return View(department);
            }


			
				

		}

	}
}
