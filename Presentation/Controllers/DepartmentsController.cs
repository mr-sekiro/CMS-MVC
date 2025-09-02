using Azure.Core;
using BusinessLogic.Data_Transfer_Object.DepartmentDtos;
using BusinessLogic.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using Newtonsoft.Json.Linq;
using Presentation.ViewModels.DepartmentsViewModel;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger, IWebHostEnvironment environment) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentService.GetAllDepartments();
            return View(departments);
        }
        #region Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) //server side Validation
            {
                try
                {
                    var createdDepartmentDto = new CreatedDepartmentDto()
                    {
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };

                    int result = departmentService.AddDepartment(createdDepartmentDto);
                    string message;
                    if (result > 0)
                    {
                        //return RedirectToAction(nameof(Index));
                        message = $"{departmentViewModel.Name} Department Created Successfully";
                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, "Department Can't Be Created");
                        message = $"{departmentViewModel.Name} Department Can not be Created";
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

            return View(departmentViewModel);

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();// 400

            var department = departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();// 404

            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();// 400

            var department = departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();// 404
            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartmetDto = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = departmentEditViewModel.Code,
                        Name = departmentEditViewModel.Name,
                        Description = departmentEditViewModel.Description,
                        DateOfCreation = departmentEditViewModel.DateOfCreation
                    };
                    int result = departmentService.UpdateDepartment(updatedDepartmetDto);
                    if (result > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not Updated");
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
            return View(departmentEditViewModel);
        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();

        //    var department = departmentService.GetDepartmentById(id.Value);

        //    if (department is null) return NotFound();

        //    return View(department);
        //}
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
                bool isDeleted = departmentService.DeleteDepartment(id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is Not Deleted!");
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
        #endregion
    }
}
