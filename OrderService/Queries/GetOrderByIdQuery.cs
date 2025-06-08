using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQuery : IRequest<Order>
{
    public int OrderId { get; set; }
}

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly OrderDbContext _context;

    public GetOrderByIdHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
    }
}