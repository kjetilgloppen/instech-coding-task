using System.ComponentModel.DataAnnotations;
using Claims.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly ClaimsService _service;

    public ClaimsController(ClaimsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Claim>> GetAsync()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Claim claim)
    {
        try
        {
            await _service.CreateAsync(claim);
            return Ok(claim);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _service.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public async Task<Claim?> GetAsync(string id)
    {
        return await _service.GetAsync(id);
    }
}
