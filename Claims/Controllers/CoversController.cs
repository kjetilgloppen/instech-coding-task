using Claims.Helpers;
using Claims.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly CoversRepository _repository;

    public CoversController(CoversRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("compute")]
    public ActionResult ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(CoversHelper.ComputePremium(startDate, endDate, coverType));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _repository.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var results = await _repository.GetAsync(id);
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        cover.Premium = CoversHelper.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _repository.CreateAsync(cover);
        return Ok(cover);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}
