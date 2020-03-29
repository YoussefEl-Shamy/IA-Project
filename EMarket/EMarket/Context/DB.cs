using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EMarket.Models;

namespace EMarket.Context
{
    public class DB : DbContext
    {
        public DbSet<Category> categoryDb { get; set; }
        public DbSet<Product> productDb { get; set; }
    }
}