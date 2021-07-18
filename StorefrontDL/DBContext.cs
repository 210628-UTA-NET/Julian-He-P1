using Microsoft.EntityFrameworkCore;
using StorefrontModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontDL
{
    public class StorefrontDBContext : DbContext
    {
        public DbSet<Storefront> Storefronts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders{ get; set;}

        public DbSet<Product> Products{ get; set;}

        public DbSet<LineItem> LineItems{get; set;}

        public StorefrontDBContext() : base()
        { }

        public StorefrontDBContext(DbContextOptions options) : base(options)
        { }

            
        protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            //It will auto generate the ID column for both tables
            p_modelBuilder.Entity<Storefront>()
                .Property(store => store.ID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Customer>()
                .Property(cust => cust.ID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Order>()
                .Property(ord => ord.OrderID)
                .ValueGeneratedOnAdd();
            
            p_modelBuilder.Entity<Product>()
                .Property(prod => prod.ID).ValueGeneratedOnAdd();
            
            p_modelBuilder.Entity<LineItem>()
                .Property(line => line.ID).ValueGeneratedOnAdd();
        }
    }
}