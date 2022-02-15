using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFrontEnd.Models
{
    public class Department
    {
        public int depId { get; set; }
        [DisplayName("Name")]
        [Required]
        public string dname { get; set; }
    }
}
