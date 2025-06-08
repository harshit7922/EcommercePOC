public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // Store hash, not plain password!
    public string Role { get; set; } = "User"; // Optional: roles
}