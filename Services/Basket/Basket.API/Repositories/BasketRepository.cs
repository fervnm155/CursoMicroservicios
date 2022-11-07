using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<ShoppingCart?> GetBasket(string username)
        {
            //obtener el valor directamente de redis
            var basket = await distributedCache.GetStringAsync(username);

            if (basket is null)
                return null;

            return System.Text.Json.JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
        {
            await distributedCache.SetStringAsync(shoppingCart.Username, JsonSerializer.Serialize(shoppingCart));

            return await GetBasket(shoppingCart.Username);
        }

        public async Task DeleteBasket(string userName)
            => await distributedCache.RemoveAsync(userName);
    }
}
