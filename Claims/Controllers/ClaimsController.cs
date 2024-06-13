using System.ComponentModel.DataAnnotations;
using Claims.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly IClaimsService _service;

    public ClaimsController(IClaimsService service)
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
    public async Task<ActionResult> DeleteAsync(string id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Claim>> GetAsync(string id)
    {
        try
        {
            var claim = await _service.GetAsync(id);
            return Ok(claim);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
