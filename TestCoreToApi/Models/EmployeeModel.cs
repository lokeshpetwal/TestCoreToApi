using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreToApi.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Dept { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
