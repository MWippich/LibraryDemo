using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.Models
{
    public class Borrow
    {

        [Key]

        public int borrowid { get; set; }

        [Required]
        [Display(Name = "Item ID")]
        public int itemid { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int userid { get; set; }

        [Required]
        [Display(Name = "Borrowing Date")]
        [DataType(DataType.Date)]

        public DateTime borrowingdate { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime expirydate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime? returndate { get; set; }


    }
}
