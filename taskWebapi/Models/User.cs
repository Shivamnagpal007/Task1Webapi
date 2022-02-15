using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }         
        public string Token { get; set; }

    }
}
