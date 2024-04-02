using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Models;
using RouteC41.G02.PL.Helpers;
using RouteC41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteC41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
       // private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public EmployeeController( IUnitOfWork unitOfWork/*IEmployeeRepository repository*/ /*,IDepartmentReposotry departmentRepo*/, IWebHostEnvironment env,IMapper mapper)
        {
            //_repository = repository;
            this.unitOfWork = unitOfWork;
            this.env = env;
            this.mapper = mapper;
        }
        public IActionResult Index(string searchInp)
        {
            //TempData.Keep();
            var employees=Enumerable.Empty<Employee>();
            var employeeRepo = unitOfWork.Repository<Employee>() as EmployeeRepository;
            #region ViewBagVsViewData
            ////Binding Through Views's Dictionary:Transfer Data From Action To View and should be sent through the action that has same view name [One way]

            ////1.ViewData
            //ViewData["Message"] = "Hello ViewData";


            ////2.ViewBag is a dynamic property based
            //ViewBag.Message = "Hello From ViewBag"; 
            #endregion
            if (string.IsNullOrEmpty(searchInp))
            {
                 employees =employeeRepo.GetAll();
          

            }
            else
            {
                 employees=employeeRepo.SearchByName(searchInp.ToLower());
                
            }
            var mappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);


        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName=DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                unitOfWork.Repository<Employee>().Add(mappedEmp);

                ///2.Update Department
                /// unitOfWork.DepartmentRepository.Update(department)
                ///delete project
                ///unitOfWork.ProjectRepository.Remove(Project);

                var Count=unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }

            }

            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = unitOfWork.Repository<Employee>().GetById(id.Value);
            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee == null)
                return NotFound();


            return View(ViewName,mappedEmp);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest("Errrorrr");

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.Repository<Employee>().Update(mappedEmp);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error Has ccured During Update The Department");

                return View(employeeVM);
            }
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {

            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.Repository<Employee>().Delete(mappedEmp);
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

                return View(employeeVM);
            }


        }
    }
}
