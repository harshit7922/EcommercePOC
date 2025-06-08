using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductController(IProductRepository repo) { _repo = repo; }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _repo.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Add(Product product) { await _repo.AddAsync(product); return Ok(product); }

    [HttpPut]
    public async Task<IActionResult> Update(Product product) { await _repo.UpdateAsync(product); return Ok(product); }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }
}