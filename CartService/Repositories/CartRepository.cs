using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly CartDbContext _context;
    public CartRepository(CartDbContext context) => _context = context;

    public async Task<Cart> GetCartByUserIdAsync(int userId)
    {
        return await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId)
            ?? new Cart { UserId = userId };
    }

    public async Task AddItemAsync(int userId, int productId, int quantity)
    {
        var cart = await GetCartByUserIdAsync(userId) ?? new Cart { UserId = userId };
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null) item.Quantity += quantity;
        else cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });

        if (cart.Id == 0) _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int userId, int productId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart == null) return;
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null) cart.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int userId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart != null) cart.Items.Clear();
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> CheckoutAsync(int userId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        // (optional) Remove or mark as checked out
        cart.Items.Clear();
        await _context.SaveChangesAsync();
        return cart;
    }
}