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
    public class EmployeeDepartment : ControllerBase
    {
        private readonly IEmployeeDepartment _empdepRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbcontext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeDepartment(IEmployeeDepartment empdepRepository, IMapper mapper, ApplicationDbcontext context, IUnitOfWork unitOfWork)
        {
            _empdepRepository = empdepRepository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;


        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_unitOfWork.EmployeDepartment.Getempdep().ToList());
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
        public IActionResult CreateEmployeDepartment([FromBody] Models.Dtos.EmployeeDepartmentDto empdepdto)
        {
            if (empdepdto == null)
                //return NotFound();
                return BadRequest();  // 400 Error
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var Employee = _mapper.Map<empdepdto, EmployeeDepartment>(empdepdto);
            if (_unitOfWork.EmployeDepartment.Create(empdepdto))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Save Data");
                return StatusCode(500, ModelState); // Server Error
            }
            return Ok();

        }
        [HttpPut]
        public IActionResult UpdateEmployeedepartment([FromBody] Models.Dtos.EmployeeDepartmentDto empdepdto)
        {
            var data = _unitOfWork.EmployeDepartment.Get(empdepdto.empId, empdepdto.depId);
            if (data == null)
                return BadRequest();

            if (empdepdto == null)
                return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var Employee = _mapper.Map<empdepdto, EmployeeDepartment>(empdepdto);
            _unitOfWork.EmployeDepartment.Update(empdepdto);
            return Ok();
        }
        [HttpDelete("{empId}")]
        public IActionResult DeleteEmployeeDepartment(int empId)
        {
            if (empId == 0)
                return NotFound();
            _unitOfWork.EmployeDepartment.Delete(empId);
            return Ok();
        }

    }
}


