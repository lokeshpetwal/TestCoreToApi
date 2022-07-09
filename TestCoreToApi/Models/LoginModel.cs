using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreToApi.Models
{
    public class LoginModel
    {
        [Key]
    
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
      
    }
}
