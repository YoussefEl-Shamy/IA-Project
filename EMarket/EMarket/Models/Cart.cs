﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Cart
    {
        /*Product ID is foreign key and primary key to create 1 to 1 relationship with product*/
        [Key]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        
        [Required]
        public DateTime added_at { get; set; }

        public virtual Product Product { get; set; }

    }
}