using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions.Baskets
{
    public interface IBasketServices
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> SetBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketAsync(string id);
    }
}
