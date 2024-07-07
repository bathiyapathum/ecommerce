using Ecommerce.Api.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
