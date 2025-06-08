using MassTransit;
using MediatR;
using Shared.Events;

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, int>
{
    private readonly OrderDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public PlaceOrderHandler(OrderDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = request.UserId,
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList(),
            OrderDate = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Publish event after order is created
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            Items = request.Items.Select(i => new Shared.Events.OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _publishEndpoint.Publish(orderCreatedEvent);

        return order.Id;
    }
}