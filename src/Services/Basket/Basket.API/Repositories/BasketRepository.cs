using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        // We are using IDistributedCache cache object as a Redis Cache
        // We have configured the Redis cache as a distributed cache in startup class
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;
            // It will deserialize the basket to the ShoppingCart Structure
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }
    // In Redis DB values are stored in key value pair. So if we did any changes to value the entire values w.r.t key 
    // will be updated. Thats why we dont need sepeare CREATE,UPDATE,PUT requests
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

    }
}
