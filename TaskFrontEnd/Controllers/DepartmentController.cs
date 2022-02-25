﻿using Microsoft.AspNetCore.Mvc;
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
    
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            
            return Json(new { data = await _departmentRepository.GetAllAsync(StaticData.DepartmentApiPath)});
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Department department = new Department();
            if (id == null)
                return View(department);
            else
            {
                department = await _departmentRepository.GetAsync(StaticData.DepartmentApiPath, id.GetValueOrDefault());
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
                    await _departmentRepository.CreateAsync(StaticData.DepartmentApiPath, department);
                }
                else
                {
                    await _departmentRepository.UpdateAsync(StaticData.DepartmentApiPath + department.depId, department);
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
            var status = await _departmentRepository.DeleteAsync(StaticData.DepartmentApiPath, id);
            if (status)
                return Json(new { success = true, message = "data successfully deleted" });
            else
                return Json(new { success = false, message = "error while delete data" });
        }
        #endregion 
    }
}
