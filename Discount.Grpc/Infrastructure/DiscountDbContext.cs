using Microsoft.EntityFrameworkCore;
using Discount.Grpc.Domain;

namespace Discount.Grpc.Infrastructure;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options) { }

    public DbSet<ProductDiscount> Discounts => Set<ProductDiscount>();
}