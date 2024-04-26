using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using System.Threading.Tasks;

namespace RouteC41.G02.PL.Controllers
{

    [Authorize]
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
        public async Task<IActionResult> Index(string searchInp)
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
                 employees =await employeeRepo.GetAllAsync();
          

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
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName=await DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                unitOfWork.Repository<Employee>().Add(mappedEmp);

                ///2.Update Department
                /// unitOfWork.DepartmentRepository.Update(department)
                ///delete project
                ///unitOfWork.ProjectRepository.Remove(Project);

                var Count= await unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }

            }

            return View(employeeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = await unitOfWork.Repository<Employee>().GetByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            if(ViewName.Equals("Delete",StringComparison.OrdinalIgnoreCase))
                 TempData["ImageName"] = employee.ImageName;

            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);


            return View(ViewName,mappedEmp);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
             return await Details(id, "Edit");

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
        public async Task <IActionResult> Delete(int? id)
        {
            return  await Details(id, "Delete");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {

            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.Repository<Employee>().Delete(mappedEmp);
               var count= await unitOfWork.Complete();
                if(count>0)
                {
                    DocumentSettings.Delete(employeeVM.ImageName, "Images");
                    return RedirectToAction(nameof(Index));

                }
                return View(employeeVM);


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
