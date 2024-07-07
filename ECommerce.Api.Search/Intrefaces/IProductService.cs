using ECommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Intrefaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsync( );
    }
}
