using System.Threading.Tasks;

namespace ECommerce.Api.Search.Intrefaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearhAsync(int customerId);
    }
}
