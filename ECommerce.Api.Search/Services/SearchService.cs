using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                var productsResult = await productsService.GetProductsAsync();
                if (productsResult.IsSuccess)
                {
                    foreach (var order in ordersResult.Orders)
                    {
                        foreach (var item in order.Items)
                        {
                            item.ProductName = productsResult.Products.FirstOrDefault(product => product.Id.Equals(item.ProductId))?.Name;
                        }
                    }
                }
                return (true, ordersResult.Orders);
            }
            return (false, null);
        }
    }
}
