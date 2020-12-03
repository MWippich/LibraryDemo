using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Programme
    {
        [Key]
        public int programmeid { get; set; }

        [Required]
        public String programmename { get; set; }
    }
}
