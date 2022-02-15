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
    public class DsgController : ControllerBase
    {
        private readonly IDsgRepository _dsgRepository;
        public DsgController(IDsgRepository dsgRepository)
        {
            _dsgRepository = dsgRepository;
        }
        [HttpGet]
        public IActionResult GetDesignation()
        {
            var Desglist = _dsgRepository.Getdsg();
                return Ok(Desglist);
           
        }
        [HttpGet("{Id:int}", Name = "Getdsg")]
        public IActionResult GetDsg(int Id)
        {
            var Designation = _dsgRepository.Getdsg(Id);
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
            if (_dsgRepository.dsgexist(designation.dsgname))
            {
                ModelState.AddModelError("", "dsg name Already In The DB");
                return StatusCode(404, ModelState); // Not Found Error
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);          
            if (!_dsgRepository.Createdsg(designation))
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
            if (_dsgRepository.dsgexist(designation.dsgname))
            {
                ModelState.AddModelError("", "dsg name Already In The DB");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);      
            if (!_dsgRepository.Updatedsg(designation))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Update Data{designation.dsgname}");
                return StatusCode(500, ModelState);
            }
            return Ok();
            
        }
        [HttpDelete("{Id:int}")]
        public IActionResult DeleteDesignation(int Id)
        {
            if (!_dsgRepository.dsgexist(Id))
                return NotFound();
            var designation = _dsgRepository.Getdsg(Id);
            if (designation == null)
                return NotFound();
            if (!_dsgRepository.Deletedsg(designation))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Deleting Data{designation.dsgname}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

    }
}
