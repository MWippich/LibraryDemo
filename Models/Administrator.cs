using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.Models
{
    public class Administrator : User
    {
        [Display(Name = "Department")]
        public String departmentname { get; set; }


    }
}
