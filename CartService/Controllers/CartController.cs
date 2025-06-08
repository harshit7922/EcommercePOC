using MassTransit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repo;
    private readonly IPublishEndpoint _publishEndpoint;

    public CartController(ICartRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
        => Ok(await _repo.GetCartByUserIdAsync(userId));

    [HttpPost("{userId}/add")]
    public async Task<IActionResult> AddItem(int userId, [FromBody] AddCartItemDto dto)
    {
        await _repo.AddItemAsync(userId, dto.ProductId, dto.Quantity);
        return Ok();
    }

    [HttpDelete("{userId}/remove/{productId}")]
    public async Task<IActionResult> RemoveItem(int userId, int productId)
    {
        await _repo.RemoveItemAsync(userId, productId);
        return Ok();
    }

    [HttpPost("{userId}/clear")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        await _repo.ClearCartAsync(userId);
        return Ok();
    }

    [HttpPost("{userId}/checkout")]
    public async Task<IActionResult> Checkout(int userId)
    {
        var cart = await _repo.CheckoutAsync(userId);
        // TODO: Optionally publish event here (see next step)
        var evt = new CartCheckedOutEvent
    {
        UserId = userId,
        Items = cart.Items.Select(i => new CartItemDto { ProductId = i.ProductId, Quantity = i.Quantity }).ToList()
    };
    await _publishEndpoint.Publish(evt);
        return Ok(cart);
    }
}

public class AddCartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}