using Basket.API.Application.Interfaces;
using Basket.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserBasket?>> GetBasket(string username)
    {
        var basket = await _repository.GetBasketAsync(username);
        return Ok(basket ?? new UserBasket { Username = username });
    }

    [HttpPost]
    public async Task<ActionResult<UserBasket>> UpdateBasket([FromBody] UserBasket basket)
    {
        var updated = await _repository.UpdateBasketAsync(basket);
        return Ok(updated);
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await _repository.DeleteBasketAsync(username);
        return NoContent();
    }
}
