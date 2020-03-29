using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Display(Name ="Category Name")]
        public string name { get; set; }
        [Display(Name = "Number Of Products")]
        public int number_of_products { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}