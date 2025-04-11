namespace Basket.API.Domain;
public class UserBasket
{
    public string Username { get; set; } = string.Empty;
    public List<UserBasketItem> Items { get; set; } = new();
}
