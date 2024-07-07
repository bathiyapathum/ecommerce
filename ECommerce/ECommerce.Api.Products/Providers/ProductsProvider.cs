//using AutoMapper;
////using ECommerce.Api.Products.Db;
//using System.Linq;
////using ECommerce.Api.Products.Db;
//using ECommerce.Api.Products.Interfaces;
//using ECommerce.Api.Products.Models;
//using Microsoft.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

using ECommerce.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ECommerce.Api.Products.Models;
using AutoMapper;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly Db.ProductDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(Db.ProductDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper) 
        { 
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData() 
        { 
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 1000 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Products.ToListAsync();

                if(products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    //var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    var result = mapper.Map<IEnumerable<Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                //var product = await dbContext.Products.SingleAsync(x => x.Id == id);
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    //var result = mapper.Map< Db.Product, Models.Product >(product);
                    var result = mapper.Map<Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Invalid ID");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
