using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFrontEnd.Models
{
    public class Employee
    {
        [Key]
        public int empId { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string ename { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string eadd { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public int esal { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int depId { get; set; }       
        public string dname { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public int dsgId { get; set; }    
        public string dsgname { get; set; }    

    }
}
