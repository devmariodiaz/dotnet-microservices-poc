using Basket.API.Domain;

namespace Basket.API.Application.Interfaces;

public interface IBasketRepository
{
    Task<UserBasket?> GetBasketAsync(string username);
    Task<UserBasket> UpdateBasketAsync(UserBasket basket);
    Task DeleteBasketAsync(string username);
}
