using Ecommerce.Api.Orders.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        //Task<(bool IsSuccess, IEnumerable<Order> order, string ErrorMessage)> GetOrdersAsync(int customerId);

        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
