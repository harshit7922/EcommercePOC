

using MassTransit;
using Shared.Events;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventConsumer> _logger;

    public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"[PaymentService] Received OrderCreatedEvent for OrderId {message.OrderId}, UserId {message.UserId}. Starting payment processing...");

        // Simulate payment logic
        await Task.Delay(500); // Simulate some processing

        _logger.LogInformation($"[PaymentService] Payment processed for OrderId {message.OrderId}.");
    }
}