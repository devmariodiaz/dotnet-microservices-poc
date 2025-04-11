using Catalog.API.Application.Interfaces;
using Catalog.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get() =>
        await _repository.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Product product)
    {
        await _repository.CreateAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
}
