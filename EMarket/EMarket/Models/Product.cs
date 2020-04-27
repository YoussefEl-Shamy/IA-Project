using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        

        [Display(Name = "Product Name")]
        [Required]
        public string name { get; set; }
        
        
        [Display(Name = "Product Price")]
        [Required]
        public float price { get; set; }
        
        
        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Please, Upload Product Photo")]
        public string image { get; set; }
        
        
        [Display(Name = "Description")]
        [Required]
        public string description { get; set; }
        
        
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        [Required(ErrorMessage = "You have to select the category")]
        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
        
        
        [NotMapped]
        public virtual int oldCategoryId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}