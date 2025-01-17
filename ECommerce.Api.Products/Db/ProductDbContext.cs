﻿using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Db
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } 

        public ProductDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
