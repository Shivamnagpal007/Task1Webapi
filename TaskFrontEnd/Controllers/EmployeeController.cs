using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;



namespace TaskFrontEnd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDesignationRepository _designationRepository;
        // private readonly IEmployeRepository _employeRepository;
        public EmployeeController(IEmployeRepository employeRepository, IDepartmentRepository departmentRepository, IDesignationRepository designationRepository)
        {
            _employeRepository = employeRepository;
            _departmentRepository = departmentRepository;
            _designationRepository = designationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _employeRepository.GetAllAsync(SD.EmployeeApiPath)});
        }

        public async Task<IActionResult> Upsert(int? id)
        {
          var DesignationList = await _designationRepository.GetAllAsync(SD.DesignationApiPath);
          var Departmentlist = await _departmentRepository.GetAllAsync(SD.DepartmentApiPath);

            if (id == null)
            {
                EmployeVM employevm = new EmployeVM()
                {
                    Employee = new Employee(),
                    DepartmentList = Departmentlist.Select(cl => new SelectListItem()
                    {
                        Text = cl.dname,
                        Value = cl.depId.ToString()
                    }),
                    DesignationList = DesignationList.Select(cl => new SelectListItem()
                    {
                        Text = cl.dsgname,
                        Value = cl.dsgId.ToString()
                    }),

                };
                return View(employevm);
            }
            else
            {
                var employees = await _employeRepository.Getasync(SD.EmployeeApiPathGetByID, id.GetValueOrDefault());

                EmployeVM employevm1 = new EmployeVM()
                {
                    Employee = new Employee()
                    {
                        empId = employees[0].empId,
                        ename=employees[0].ename,
                        esal = employees[0].esal,
                        eadd= employees[0].eadd,
                        depId = employees[0].depId,
                        dname = employees[0].dname,
                       dsgId = employees[0].dsgId,
                       dsgname = employees[0].dsgname,
                                
                    },
                    DepartmentList = Departmentlist.Select(cl => new SelectListItem()
                    {
                        Text = cl.dname,
                        Value = cl.depId.ToString()
                    }),
                    DesignationList = DesignationList.Select(cl => new SelectListItem()
                    {
                        Text = cl.dsgname,
                        Value = cl.dsgId.ToString()
                    }),

                };

                if (employees == null)

                    return NotFound();
                return View(employevm1);
            }     
           
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Employee  employee)
        {

            if (ModelState.IsValid)
            {
                if (employee.empId == 0)
                {
                    await _employeRepository.CreateAsync(SD.EmployeeApiPath,employee);
                }
                else
                {
                    Employee receivedEmployee = new Employee();

                    var httpClient = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:44335/api/EmpDep")
                    {
                        Content = new StringContent(new JavaScriptSerializer().Serialize(employee), Encoding.UTF8, "application/json")
                    };

                    var response = await httpClient.SendAsync(request);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedEmployee = JsonConvert.DeserializeObject<Employee>(apiResponse);                
                    //await _employeRepository.UpdateAsync(SD.EmployeeApiPath + employee.empId, employee);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employee);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
           // var depid = _employeRepository.GetAsync(SD.EmployeeApiPathGetByID,id);
            var status = await _employeRepository.DeleteAsync(SD.EmployeeApiPath, id);
            if (status)
              return Json(new { success = true, message = "data successfully deleted" });
            else
               return Json(new { success = false, message = "error while delete data" });
        }
    }
}
