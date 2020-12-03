using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{

    public class User
    {

        public static User LoggedInUser;
        public static bool Administrator;

        [Key]
        public int userid { set; get; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public String username { set; get; }

        [MaxLength(50)]
        [EmailAddress]
        [Display(Name = "Email Account")]
        public String emailaccount { get; set; }

        [MaxLength(15), Phone]

        [Display(Name = "Phone Number")]
        public String phonenumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime dateofbirth { get; set; }
        
        [MaxLength(255)]
        [Display(Name = "Address")]
        public String address { get; set; }
    }
}
