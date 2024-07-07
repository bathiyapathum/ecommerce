using ECommerce.Api.Search.Intrefaces;
using ECommerce.Api.Search.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearhAsync(int customerId)
        {
            var customersResult = await customerService.GetCustomer(customerId);
            var orderResult = await orderService.GetOrderAsync(customerId);
            var productResult = await productService.GetProductAsync();

            if(orderResult.IsSuccess)
            {
                foreach(var order in orderResult.Order)
                {
                    foreach(var item in order.Items)
                        {
                        item.ProductName = productResult.IsSuccess ? 
                            productResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                        "Product information is not avaiable";
                        }
                }

                var result = new
                {
                    Customer = customersResult.IsSuccess?
                    customersResult.customer:
                    new {Name = "Customer Information is not available"},
                    Orders = orderResult.Order
                };

                return (true, result);
            }

            return (false,null);
        }
    }
}
