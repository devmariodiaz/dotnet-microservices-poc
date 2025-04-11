using Discount.Grpc.Domain;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Infrastructure;

public class DiscountRepository
{
    private readonly DiscountDbContext _context;

    public DiscountRepository(DiscountDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDiscount?> GetByProductIdAsync(string productId)
    {
        return await _context.Discounts.FirstOrDefaultAsync(d => d.ProductId == productId);
    }
}
