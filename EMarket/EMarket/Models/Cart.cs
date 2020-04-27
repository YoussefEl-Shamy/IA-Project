using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        
        [Required]
        public DateTime added_at { get; set; }

        [NotMapped]
        public TimeSpan diff
        {
            get
            {
                return DateTime.Now - this.added_at;
            }
        }

        public virtual Product Product { get; set; }

    }
}