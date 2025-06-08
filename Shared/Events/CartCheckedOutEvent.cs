public class CartCheckedOutEvent
{
    public int UserId { get; set; }
    public List<CartItemDto> Items { get; set; }
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}