using Microsoft.EntityFrameworkCore;
using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class ProductOrderDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Orders> Orders { get; set; }


        public ProductOrderDbContext(DbContextOptions<ProductOrderDbContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.ID); // For Product_Id is Primary Key
            

            modelBuilder.Entity<Product>().HasMany(o=>o.Orders).WithOne(p=>p.Products)
                .HasForeignKey(p=>p.ProductID); // Foreign Key Relationship 

            modelBuilder.Entity<Orders>().HasKey(o => o.ID);// For Order_Id is Primary Key
          

            modelBuilder.Entity<Orders>().HasOne(e => e.Products).WithMany(d => d.Orders)
                     .HasForeignKey(e => e.ProductID); // Foreign Key Product_Id
        }


       

      
    }
}
