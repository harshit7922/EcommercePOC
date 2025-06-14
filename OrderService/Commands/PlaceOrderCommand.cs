using MediatR;

public class PlaceOrderCommand : IRequest<int>
{
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}