using AddProduct.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddProduct.Core.Contexts
{
    public class OneDollarContext : DbContext
    {
        public OneDollarContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = Products.db");


            }
        }
    }
}
