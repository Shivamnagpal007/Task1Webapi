using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFrontEnd.Models
{
    public class EmployeVM
    {
       public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        public IEnumerable<SelectListItem> DesignationList { get; set; }
    }
}
