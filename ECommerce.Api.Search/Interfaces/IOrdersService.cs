using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSucess, IEnumerable<Models.Order> Orders, string ErroMessage)> GetOrdersAsync(int customerId);
    }
}
