using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.Models
{
    public class Fine
    {
        [Key]
        public int borrowid { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int amount { get; set; }

        public bool paid { get; set; }
    }
}
