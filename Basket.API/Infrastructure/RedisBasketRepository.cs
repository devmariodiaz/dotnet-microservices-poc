using System.Text.Json;
using Basket.API.Application.Interfaces;
using Basket.API.Domain;
using StackExchange.Redis;

namespace Basket.API.Infrastructure;

public class RedisBasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public RedisBasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<UserBasket?> GetBasketAsync(string username)
    {
        var basket = await _database.StringGetAsync(username);
        if (basket.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<UserBasket>(basket!);
    }

    public async Task<UserBasket> UpdateBasketAsync(UserBasket basket)
    {
        var json = JsonSerializer.Serialize(basket);
        await _database.StringSetAsync(basket.Username, json);
        return basket;
    }

    public async Task DeleteBasketAsync(string username)
    {
        await _database.KeyDeleteAsync(username);
    }
}
