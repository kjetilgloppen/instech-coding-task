using Claims.Helpers;
using Claims.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly CoversService _service;

    public CoversController(CoversService service)
    {
        _service = service;
    }

    [HttpPost("compute")]
    public ActionResult ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(CoversHelper.ComputePremium(startDate, endDate, coverType));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _service.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var results = await _service.GetAsync(id);
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        await _service.CreateAsync(cover);
        return Ok(cover);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _service.DeleteAsync(id);
    }
}
