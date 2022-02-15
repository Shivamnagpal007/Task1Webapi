using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Models
{
    public class Employee
    {
        [Key]
        public int empId { get; set; }
        public string ename { get; set; }
        public string eadd { get; set; }
        public int esal { get; set; }
        public int dsgId { get; set; }
        [ForeignKey("dsgId")]  
        public Designation Designation { get; set; }
       


    }
}
