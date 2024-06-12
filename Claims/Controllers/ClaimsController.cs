using Claims.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly ClaimsRepository _repository;

    public ClaimsController(ClaimsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Claim>> GetAsync()
    {
        return await _repository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Claim claim)
    {
        await _repository.CreateAsync(claim);
        return Ok(claim);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public async Task<Claim?> GetAsync(string id)
    {
        return await _repository.GetAsync(id);
    }
}
