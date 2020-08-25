using System;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool Issucess, dynamic SearchResults)> SearchAsync(int customerId);
    }
}
