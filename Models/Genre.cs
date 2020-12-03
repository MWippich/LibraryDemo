using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Genre
    {
        [Key]
        public int genreid { get; set; }

        [Required]
        [MaxLength(50)]
        public string genre { get; set; }
    }
}
