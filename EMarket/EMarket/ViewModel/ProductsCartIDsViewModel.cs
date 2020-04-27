using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMarket.Models;

namespace EMarket.ViewModel
{
    public class ProductsCartIDsViewModel
    {
        public List<Product> products { get; set; }
        public List<int> cartProductsIDs { get; set; }
        public bool inCart { get; set; }
    }
}