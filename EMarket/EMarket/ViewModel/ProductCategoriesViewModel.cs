using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMarket.Models;

namespace EMarket.ViewModel
{
    public class ProductCategoriesViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}