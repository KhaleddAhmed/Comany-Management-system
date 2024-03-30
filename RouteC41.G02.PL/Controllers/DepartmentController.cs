using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Models;
using RouteC41.G02.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
namespace RouteC41.G02.PL.Controllers
{
	//Inheritance : Department Controller is a Controller
	//Association :Department Controller Has a DepartmentRepostry
	public class DepartmentController : Controller
	{
        private readonly IUnitOfWork unitOfWork;

        //private readonly IDepartmentReposotry _repostry;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork/*IDepartmentReposotry departmentrepostry*/,IWebHostEnvironment env,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            //_repostry = departmentrepostry;
            this.env = env;
            this.mapper = mapper;
        }

        // /Department/Index
        public IActionResult Index()
		{
			var departments=unitOfWork.Repository<Department>().GetAll();
            var mappedDepartments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
			return View(mappedDepartments);
        }


		[HttpGet]
		public IActionResult Create()
		{
			
			return View();
		}

		[HttpPost]
		public IActionResult Create(DepartmentViewModel departmentVM)
		{
            var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
            if (ModelState.IsValid)//Server side validation
			{
				unitOfWork.Repository<Department>().Add(mappedDep);
				var count = unitOfWork.Complete();
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			return View(mappedDep);

		}

		[HttpGet]
		public IActionResult Details(int? id,string ViewName="Details")
		{
			if (/*id is null*/ !id.HasValue)
				return BadRequest();
			var department = unitOfWork.Repository<Department>().GetById(id.Value);
			if(department is null)
				return NotFound();

            var mappedDep = mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, mappedDep);

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
		public IActionResult Edit([FromRoute] int id,DepartmentViewModel departmentVM)
		{

            var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
            if (id != departmentVM.Id)
				return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(departmentVM);

			}
			
			try
			{
                unitOfWork.Repository<Department>().Update(mappedDep);
				unitOfWork.Complete();
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

				return View (departmentVM);
			}

		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(DepartmentViewModel departmentVM)
		{
          
            try
			{
				var mappedDep=mapper.Map<DepartmentViewModel,Department>(departmentVM);
				unitOfWork.Repository<Department>().Delete(mappedDep);
				unitOfWork.Complete();
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
				
                    return View(departmentVM);
            }


			
				

		}

	}
}
