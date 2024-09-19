using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryManagementApp.Models
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }

        public InventoryDbContext() : base("InventoryDB")
        {
        }
    }
}