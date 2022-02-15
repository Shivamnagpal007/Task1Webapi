using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IDepartmentRepository departmentRepository, ILogger<DepartmentController> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            _logger.LogTrace("this is dep trace log");
            return Json(new { data = await _departmentRepository.GetAllAsync(SD.DepartmentApiPath) });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Department department = new Department();
            if (id == null)
                return View(department);
            else
            {
                department = await _departmentRepository.GetAsync(SD.DepartmentApiPath, id.GetValueOrDefault());
                if (department == null)
                    return NotFound();
                return View(department);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.depId == 0)
                {
                    await _departmentRepository.CreateAsync(SD.DepartmentApiPath, department);
                }
                else
                {
                    await _departmentRepository.UpdateAsync(SD.DepartmentApiPath + department.depId, department);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(department);
            }
        }
        #region API Call
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var status = await _departmentRepository.DeleteAsync(SD.DepartmentApiPath, id);
            if (status)
                return Json(new { success = true, message = "data successfully deleted" });
            else
                return Json(new { success = false, message = "error while delete data" });
        }
        #endregion 
    }
}
