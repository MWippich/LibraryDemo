using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Item
    {

        [Key]
        public int itemid { get; set; }

        public int publishingid { get; set; }
    }
}
