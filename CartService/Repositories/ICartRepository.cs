public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(int userId);
    Task AddItemAsync(int userId, int productId, int quantity);
    Task RemoveItemAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
    Task<Cart> CheckoutAsync(int userId);  // For checkout
}