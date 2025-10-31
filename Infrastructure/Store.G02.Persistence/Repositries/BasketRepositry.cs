﻿using StackExchange.Redis;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repositries
{
    public class BasketRepositry(IConnectionMultiplexer connection) : IBasketRepositry
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;
            
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if (basket is null) return null;

            return basket;
        }

        public async Task<CustomerBasket?> SetBasketAsync(CustomerBasket basket, TimeSpan? timeToLive)
        {
            var redisValue = JsonSerializer.Serialize(basket);
            var flag = await _database.StringSetAsync(basket.Id, redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(basket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

    }
}
