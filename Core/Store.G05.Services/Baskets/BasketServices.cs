using AutoMapper;
using Store.G02.Shared.Dtos;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Baskets;
using Store.G05.Domain.Exceptions;
using Store.G05.Services.Abstractions.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Baskets
{
    public class BasketServices(IBasketRepositry basketRepositry, IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await basketRepositry.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<BasketDto?> SetBasketAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepositry.SetBasketAsync(basket);
            if (basket is null) throw new BasketCreateOrUpdateBadRequestException();
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag = await basketRepositry.DeleteBasketAsync(id);
            if (flag == false) throw new BasketDeleteBadRequestException();
            return flag;
        }

    }
}
