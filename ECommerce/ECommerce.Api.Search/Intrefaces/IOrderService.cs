using ECommerce.Api.Search.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Intrefaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Order, string ErrorMessage)> GetOrderAsync(int customerId);
    }
}
