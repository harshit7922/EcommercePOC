using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    public UserRepository(UserDbContext context) { _context = context; }

    public async Task<User> GetByUsernameAsync(string username) => 
        await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User> GetByEmailAsync(string email) => 
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}