using BusinessLogic.Data_Transfer_Object;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.DepartmentsViewModel;

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
        public IActionResult Create(CreatedDepartmentDto createdDepartmentDto)
        {
            if (ModelState.IsValid) //server side Validation
            {
                try
                {
                    int result = departmentService.AddDepartment(createdDepartmentDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created");
                    }
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

            return View(createdDepartmentDto);

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
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentEditViewModel)
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
