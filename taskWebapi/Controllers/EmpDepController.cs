using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Controllers
{
    [Route("api/EmpDep")]
    [ApiController]
    //[Authorize]
    public class EmpDepController : ControllerBase
    {
        private readonly IempDepRepository _empdepRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbcontext _context;
       
        public EmpDepController(IempDepRepository empdepRepository,IMapper mapper, ApplicationDbcontext context)
        {
            _empdepRepository = empdepRepository;
            _mapper = mapper;
            _context=context;
         
        }
        //[HttpGet("GetEmployeBypredicate")]
        //public IActionResult GetEmployeesByPredicate(string Name, string Address)
        //{
        //    var predicate = PredicateBuilder.New<EmployeeDepartment>();
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        predicate = predicate.And(i => i.Employee.ename.ToLower().StartsWith(Name) || i.Employee.eadd.ToLower().StartsWith(Address));
        //    }
        //    var employees = _context.EmployeeDepartments.Where(predicate).Select(i => i).Include(p => p.Employee.ename).Include(p => p.Employee.eadd).ToList();
        //    return Ok(employees);
            
        //}
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_empdepRepository.Getempdep());
        }
        [HttpGet("GetEmployeeByid/{id}")]
        public IActionResult GetEmployeeByID(int id)
        {
            var Employee = _empdepRepository.GetempdepId(id);
            if (Employee.Count == 0)
            {
                return StatusCode(404, ModelState);
            }              
            return Ok(Employee);
        }
        [HttpPost]
        public IActionResult CreateEmployeDepartment([FromBody] empdepdto empdepdto)
        {
            if (empdepdto == null)
                //return NotFound();
                return BadRequest();  // 400 Error
            if (!ModelState.IsValid) return BadRequest(ModelState);
         // var Employee = _mapper.Map<empdepdto, EmployeeDepartment>(empdepdto);
            if (_empdepRepository.Createempdep(empdepdto))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Save Data");
                return StatusCode(500, ModelState); // Server Error
            }
            return Ok();

        }
        [HttpPut]
        public IActionResult UpdateEmployeedepartment([FromBody] empdepdto empdepdto)
        {
            var data = _empdepRepository.Get(empdepdto.empId,empdepdto.depId);
            if (data == null)
                return BadRequest();

            if (empdepdto == null)
                return BadRequest();         
            if (!ModelState.IsValid) return BadRequest(ModelState);
             //var Employee = _mapper.Map<empdepdto, EmployeeDepartment>(empdepdto);
            _empdepRepository.Update(empdepdto);
            return Ok();
        }
        [HttpDelete("{empId}")]
        public IActionResult DeleteEmployeeDepartment(int empId)
        {
            if (empId == 0)
                return NotFound();           
            _empdepRepository.Deleteempdep(empId);         
            return Ok();
        }
    }
}
