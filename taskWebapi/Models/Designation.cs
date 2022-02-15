using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Models
{
    public class Designation
    {
        [Key]
        public int dsgId { get; set; }
        public string dsgname { get; set; }
    }
}
