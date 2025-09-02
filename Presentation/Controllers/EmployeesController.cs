using BusinessLogic.Data_Transfer_Object;
using BusinessLogic.Data_Transfer_Object.EmployeeDtos;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ViewModels.DepartmentsViewModel;
using Presentation.ViewModels.EmployeesViewModel;
using System;

namespace Presentation.Controllers
{
    public class EmployeesController(IEmployeeService employeeService, ILogger<DepartmentsController> logger, IWebHostEnvironment environment) : Controller
    {
        public IActionResult Index()
        {
            var employees = employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid) //server side Validation
            {
                try
                {
                    var createdEmployeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Salary = employeeViewModel.Salary,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType
                    };
                    int result = employeeService.AddEmployee(createdEmployeeDto);
                    string message;
                    if (result > 0)
                    {
                        //return RedirectToAction(nameof(Index));
                        message = $"{employeeViewModel.Name} Created Successfully";
                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, "Employee Can't Be Created");
                        message = $"{employeeViewModel.Name} Can't Be Created";
                    }
                    TempData["Message"] = message;
                    TempData["Result"] = result;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1.Development => log Error In Console and Return Same View With Eerror Message
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    // 2.Deployment => log Error in File | Table in DB and Return Error View
                    else
                    {
                        logger.LogError(ex.Message);
                    }
                }
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();// 400

            var employee = employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();// 404

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();// 400

            var employee = employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();// 404
            return View(new EmployeeViewModel()
            {

                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedEmployeeDto = new UpdatedEmployeeDto()
                    {
                        Id = id,
                        Name = employeeViewModel.Name,
                        Salary = employeeViewModel.Salary,
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType
                    };
                    int result = employeeService.UpdateEmployee(updatedEmployeeDto);
                    if (result > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee is not Updated");
                    }

                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }
                }
            }
            return View(employeeViewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                bool isDeleted = employeeService.DeleteEmployee(id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is Not Deleted!");
                    return RedirectToAction(actionName: nameof(Delete), new { id });

                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
    }
}
