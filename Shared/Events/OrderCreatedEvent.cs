using System;
using System.Collections.Generic;


namespace Shared.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public DateTime OrderDate { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}