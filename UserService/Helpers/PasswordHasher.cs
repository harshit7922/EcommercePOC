using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hash == hashOfInput;
    }
}