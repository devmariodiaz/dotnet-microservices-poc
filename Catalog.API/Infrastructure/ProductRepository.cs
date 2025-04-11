using Catalog.API.Application.Interfaces;
using Catalog.API.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        var database = client.GetDatabase("CatalogDb");
        _collection = database.GetCollection<Product>("Products");
    }

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _collection.Find(p => true).ToListAsync();

    public async Task<Product?> GetByIdAsync(string id) =>
        await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product product) =>
        await _collection.InsertOneAsync(product);
}
