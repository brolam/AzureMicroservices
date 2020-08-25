using System;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        public async Task<(bool Issucess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            await Task.Delay(1);
            return (true, new {Message = "Hello"});
        }
    }
}
