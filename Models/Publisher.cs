using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Publisher
    {
        [Key]
        public int publisherid { get; set; }

        [Required]
        [MaxLength(50)]
        public String publishername { get; set; }


    }
}
