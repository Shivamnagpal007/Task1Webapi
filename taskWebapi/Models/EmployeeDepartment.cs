using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Models
{
    public class EmployeeDepartment
    {
        [Key]
        public int empId { get; set; }
        public Employee Employee { get; set; }
        [Key]
        public int depId { get; set; }
        public Department Department { get; set; }


    }
}
