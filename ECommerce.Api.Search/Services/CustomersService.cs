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
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessagem)> GetCustomer(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("Customers");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<Customer>(content, options);
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
