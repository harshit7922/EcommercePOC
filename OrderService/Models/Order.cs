public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
