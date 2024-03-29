using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Models;
using System;

namespace RouteC41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment env;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            this.env = env;
        }
        public IActionResult Index()
        {
            #region ViewBag vs ViewData
            ////Binding Through Views's Dictionary:Transfer Data From Action To View and should be sent through the action that has same view name [One way]

            ////1.ViewData
            //ViewData["Message"] = "Hello ViewData";


            ////2.ViewBag is a dynamic property based
            //ViewBag.Message = "Hello From ViewBag"; 
            #endregion

            var departments = _repository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var Count = _repository.Add(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = _repository.GetById(id.Value);
            if (employee == null)
                return NotFound();


            return View(employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("Errrorrr");

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _repository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error Has ccured During Update The Department");

                return View(employee);
            }
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _repository.GetById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {

            try
            {
                _repository.Delete(employee);
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

                return View(employee);
            }


        }
    }
}
