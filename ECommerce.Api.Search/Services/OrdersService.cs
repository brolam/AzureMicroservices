using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Search.Services
{
    public class OrdersService: IOrdersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrdersService> logger;

        public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSucess, IEnumerable<Order> Orders, string ErroMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersService");
                var url = $"api/orders/{customerId}";
                logger?.LogInformation(url);
                var response = await client.GetAsync(url);
                if ( response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, option);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (System.Exception exception)
            {
                logger?.LogError(exception.ToString());
                return (false, null, exception.Message);
            }
        }
    }
}
