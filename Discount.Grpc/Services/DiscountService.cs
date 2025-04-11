using Discount.Grpc;
using Discount.Grpc.Infrastructure;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountServiceImpl : DiscountService.DiscountServiceBase
{
    private readonly DiscountRepository _repository;

    public DiscountServiceImpl(DiscountRepository repository)
    {
        _repository = repository;
    }

    public override async Task<DiscountResponse> GetDiscount(DiscountRequest request, ServerCallContext context)
    {
        var discount = await _repository.GetByProductIdAsync(request.ProductId);

        if (discount is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"No discount found for product: {request.ProductId}"));
        }

        return new DiscountResponse
        {
            ProductId = discount.ProductId,
            Description = discount.Description,
            Amount = (double)discount.Amount
        };
    }
}
