using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        //private readonly ILogger<CustomersProvider> logger;
        //private readonly IMapper mapper;
        //private readonly CustomerDbContext dbContext;

        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;


        public CustomersProvider(CustomerDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }



        private void SeedData()
        {
            if (!dbContext.Customers.Any()) 
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Jessica Smith", Address = "20 Elm St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "John Smith", Address = "30 Main St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "William Johnson", Address = "100 10th St." });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
                try
                {
                    logger?.LogInformation("Quering customers");
                    var Customers = await dbContext.Customers.ToListAsync();

                    if (Customers != null && Customers.Any())
                    {
                    logger?.LogInformation($"{Customers.Count} customers(s) found");
                        var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(Customers);
                        return (true, result, null);
                    }
                    return (false, null, "Not Found");
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.ToString());
                    return (false, null, ex.Message);
                }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    logger?.LogInformation("Customer found");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
