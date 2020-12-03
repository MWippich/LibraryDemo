using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.Models
{
    public class Book
    {
        [Key]
        public int bookid { get; set; }

        [Display(Name = "Prequel")]
        public int? prequelid { get; set; }

        public int bookinfoid { get; set; }

        public int publishingid { get; set; }

        public int publisherid { get; set; }

        [Display(Name = "Series")]
        public String? series { get; set; }

        [Required]
        [Display(Name = "Title")]
        public String? title { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public String? isbn { get; set; }

        [Display(Name = "Edition")]
        public String? edition { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Publication Date")]
        public DateTime? publishingdate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Must be a positive integer")]
        [Display(Name = "Page Count")]
        public int? pagecount { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public String? publishername { get; set; }

        [Display(Name = "Language")]
        public String? language { get; set; }

        [Display(Name = "Authors")]
        public String? authors { get; set; }

        [Display(Name = "Genres")]
        public String? genres { get; set; }

        [Display(Name = "Number Of Copies")]
        public int? numcopies { get; set; }

        [Display(Name = "Unavailble Copies")]
        public int? unavailblecopies { get; set; }

    }
}
