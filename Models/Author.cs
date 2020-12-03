using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Author
    {
        [Key]
        public int authorid { get; set; }

        [Required]
        public String authorname { get; set; }


    }
}
