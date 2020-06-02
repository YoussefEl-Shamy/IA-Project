using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMarket.Models;

namespace EMarket.ViewModel
{
    /*That view model is created to relate both of the product with a list of IDs of products that exixst in the car, and it is used in the products list to know which product exists in the cart */
    public class ProductsCartIDsViewModel
    {
        public List<Product> products { get; set; }
        public List<int> cartProductsIDs { get; set; }
        public bool inCart { get; set; }
    }
}