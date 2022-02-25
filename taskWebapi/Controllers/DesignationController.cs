using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Controllers
{
    [Route("api/Dsg")]
    [ApiController]
    //[Authorize]
    public class DesignationController : ControllerBase
    {
        //private readonly IDsgRepository _dsgRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DesignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetDesignation()
        {
            var Desglist = _unitOfWork.Designation.GetAll().ToList();
                return Ok(Desglist);
           
        }
        [HttpGet("{Id:int}", Name = "Getdsg")]
        public IActionResult GetDsg(int Id)
        {
            var Designation = _unitOfWork.Designation.Get(Id);
            if (Designation == null)
                return StatusCode(404, ModelState);                  
            return Ok(Designation);
        }
        [HttpPost]
        public IActionResult CreateDesignation([FromBody] Designation designation) 
        {
            if (designation == null)
                //return NotFound();
                return BadRequest();  // 400 Error
            if (_unitOfWork.Designation.DesignationExistByName(designation.dsgname))
            {
                ModelState.AddModelError("", "Department name Already In The DB");
                return StatusCode(404, ModelState); // Not Found Error
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unitOfWork.Designation.Add(designation))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Save Data{designation.dsgname}");
                return StatusCode(500, ModelState); // Server Error
            }
            return Ok();

        }
        [HttpPut("{Id:int}")]
        public IActionResult UpdateDesignation(int Id, [FromBody] Designation designation)
        {
            if (designation == null)
                return BadRequest();
            if (_unitOfWork.Designation.DesignationExistByName(designation.dsgname))
            {
                ModelState.AddModelError("", "dsg name Already In The DB");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unitOfWork.Designation.Update(designation))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Update Data{designation.dsgname}");
                return StatusCode(500, ModelState);
            }
            return Ok();

        }
        [HttpDelete("{Id:int}")]
        public IActionResult DeleteDesignation(int Id)
        {
            if (!_unitOfWork.Designation.DesignationExistById(Id))
                return NotFound();
            var Designation = _unitOfWork.Designation.Get(Id);
            if (Designation == null)
                return NotFound();
            if (!_unitOfWork.Designation.Remove(Designation))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Deleting Data{Designation.dsgname}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

    }
}
