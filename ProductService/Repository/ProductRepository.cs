using Microsoft.EntityFrameworkCore;


public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;
    public ProductRepository(ProductDbContext context) { _context = context; }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
    public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    public async Task AddAsync(Product product) { await _context.Products.AddAsync(product); await _context.SaveChangesAsync(); }
    public async Task UpdateAsync(Product product) { _context.Products.Update(product); await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(int id) { var prod = await _context.Products.FindAsync(id); if (prod != null) { _context.Products.Remove(prod); await _context.SaveChangesAsync(); } }
}