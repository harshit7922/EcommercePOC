using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly IConfiguration _config;

    public AuthController(IUserRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _repo.GetByUsernameAsync(dto.Username) != null)
            return BadRequest("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = PasswordHasher.HashPassword(dto.Password)
        };
        await _repo.AddAsync(user);

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _repo.GetByUsernameAsync(dto.Username);
        if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        var token = JwtHelper.GenerateJwtToken(user, _config["Jwt:Secret"]);
        return Ok(new { token });
    }
}