using Store.G05.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Contracts
{
    public interface IBasketRepositry
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> SetBasketAsync(CustomerBasket basket, TimeSpan? timeToLive);
        Task<bool> DeleteBasketAsync(string id);
    }
}
