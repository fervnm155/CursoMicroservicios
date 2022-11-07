using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task DeleteBasket(string userName);
        Task<ShoppingCart?> GetBasket(string username);
        Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart);
    }
}