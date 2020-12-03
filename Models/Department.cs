using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Department
    {

        [Key]
        public int departmentid { get; set; }

        [Required]
        public String departmentname { get; set; }

    }
}
