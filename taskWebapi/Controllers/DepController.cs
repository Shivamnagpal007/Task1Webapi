using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Controllers
{
    [Route("api/Dep")]
    [ApiController]
    public class DepController : ControllerBase
    {
        private readonly IDepRepository _depRepository;
        
        public DepController(IDepRepository depRepository)
        {
            _depRepository = depRepository;
            
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
          
            var Deplist = _depRepository.Getdep();
            return Ok(Deplist);

        }
        [HttpGet("{Id:int}", Name = "Getdep")]
        public IActionResult GetDepatment(int Id)
        {

            var Department = _depRepository.Getdep(Id);
            if (Department == null)
                return StatusCode(404, ModelState);
            return Ok(Department);
        }
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] Department Department)
        {
            if (Department == null)
                //return NotFound();
                return BadRequest();  // 400 Error
            if (_depRepository.depexist(Department.dname))
            {
                ModelState.AddModelError("", "Department name Already In The DB");
                return StatusCode(404, ModelState); // Not Found Error
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_depRepository.Createdep(Department))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Save Data{Department.dname}");
                return StatusCode(500, ModelState); // Server Error
            }
            return Ok();

        }
        [HttpPut("{Id:int}")]
        public IActionResult UpdateDepartment(int Id, [FromBody] Department Department)
        {
            if (Department == null)
                return BadRequest();
            if (_depRepository.depexist(Department.dname))
            {
                ModelState.AddModelError("", "Department name Already In The DB");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_depRepository.Updatedep(Department))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Update Data{Department.dname}");
                return StatusCode(500, ModelState);
            }
            return Ok();

        }
        [HttpDelete("{Id:int}")]
        public IActionResult DeleteDepartment(int Id)
        {
            if (!_depRepository.depexist(Id))
                return NotFound();
            var Department = _depRepository.Getdep(Id);
            if (Department == null)
                return NotFound();
            if (!_depRepository.Deletedep(Department))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Deleting Data{Department.dname}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
