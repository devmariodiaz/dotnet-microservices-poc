namespace Discount.Grpc.Domain;

public class ProductDiscount
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
