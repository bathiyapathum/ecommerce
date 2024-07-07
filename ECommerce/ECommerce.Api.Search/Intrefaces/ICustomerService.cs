using ECommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Intrefaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, dynamic customer, string ErrorMessage)> GetCustomer(int id);
    }
}
