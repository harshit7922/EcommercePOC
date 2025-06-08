using MassTransit;
using Shared.Events;

public class CartCheckedOutEventConsumer : IConsumer<CartCheckedOutEvent>
{
    private readonly OrderDbContext _dbContext;
    private readonly ILogger<CartCheckedOutEventConsumer> _logger;

    public CartCheckedOutEventConsumer(OrderDbContext dbContext, ILogger<CartCheckedOutEventConsumer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CartCheckedOutEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Received CartCheckedOutEvent for UserId {message.UserId}");

        // Create Order
        var order = new Order
        {
            UserId = message.UserId,
            OrderDate = DateTime.UtcNow,
            Items = message.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Order created with Id {order.Id} for UserId {order.UserId}");
        // (Optional) publish OrderCreatedEvent for PaymentService, etc.
    }
}