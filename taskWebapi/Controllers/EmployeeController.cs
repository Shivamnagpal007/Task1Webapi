using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeRepository _EmployeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeRepository EmployeRepository, IMapper mapper)
        {
            _EmployeRepository = EmployeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            return Ok(_EmployeRepository.GetEmploye().Select(_mapper.Map<Employee, EmployeDisplayDto>));
        }
        [HttpGet("{empId:int}", Name = "Getemp")]
        public IActionResult GetEmployee(int empId)
        {
            var Employee = _EmployeRepository.GetEmployee(empId);
            if (Employee == null)
                return StatusCode(404, ModelState);
            return Ok(Employee);
        }
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeDisplayDto employeDisplayDto)
        {
            if (employeDisplayDto == null)
                //return NotFound();
                if (!ModelState.IsValid) return BadRequest(ModelState);
            var Employee = _mapper.Map<EmployeDisplayDto, Employee>(employeDisplayDto);
            if (!_EmployeRepository.CreateEmployee(Employee))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Save Data");
                return StatusCode(500, ModelState); // Server Error
            }
            return Ok();

        }
        [HttpPatch]
        public IActionResult UpdateEmployee(int EmpId, [FromBody] EmployeDisplayDto employeDisplayDto)
        {
            if (employeDisplayDto == null)
                return BadRequest();      
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Employee = _mapper.Map<EmployeDisplayDto, Employee>(employeDisplayDto);
            if (!_EmployeRepository.UpdateEmployee(Employee))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Update Data");
                return StatusCode(500, ModelState);
            }
            return Ok();

        }
        [HttpDelete]
        public IActionResult DeleteEmployee(int empId)
        {
            if (empId == 0)
                return NotFound();
            var Employee = _EmployeRepository.GetEmployee(empId);
            if (!_EmployeRepository.DeleteEmployee(Employee))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong While Deleting Data");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

    }
}
