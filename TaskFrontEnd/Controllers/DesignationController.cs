using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationRepository _designationRepository;
        public DesignationController(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _designationRepository.GetAllAsync(SD.DesignationApiPath)});
        }
        
        public async Task<IActionResult> Upsert(int? id)
        {
            Designation designation = new Designation();
            if (id == null)
                return View(designation);
            else
            {
                designation = await _designationRepository.GetAsync(SD.DesignationApiPath, id.GetValueOrDefault());
                if (designation == null)
                    return NotFound();
                return View(designation);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Designation designation)
        {
             if (ModelState.IsValid)
            {
                if (designation.dsgId == 0)
                {
                    await _designationRepository.CreateAsync(SD.DesignationApiPath, designation);
                }
                else
                {
                    await _designationRepository.UpdateAsync(SD.DesignationApiPath +designation.dsgId, designation);
                }
                return RedirectToAction(nameof(Index));
            }                              
            else
            {
                return View(designation);
            }
        }
        #region API Call
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var status = await _designationRepository.DeleteAsync(SD.DesignationApiPath, id);
            if (status)
                return Json(new { success = true, message = "data successfully deleted" });
            else
                return Json(new { success = false, message = "error while delete data" });
        }
        #endregion 
    }
}
