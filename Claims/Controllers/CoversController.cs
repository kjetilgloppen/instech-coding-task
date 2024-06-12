using Claims.Auditing;
using Claims.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly ClaimsContext _claimsContext;
    private readonly Auditer _auditer;

    public CoversController(ClaimsContext claimsContext, AuditContext auditContext)
    {
        _claimsContext = claimsContext;
        _auditer = new Auditer(auditContext);
    }

    [HttpPost("compute")]
    public ActionResult ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(CoversHelper.ComputePremium(startDate, endDate, coverType));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _claimsContext.GetAllCoversAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var results = await _claimsContext.GetCoverAsync(id);
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        cover.Id = Guid.NewGuid().ToString();
        cover.Premium = CoversHelper.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _claimsContext.AddCoverAsync(cover);
        _auditer.AuditCover(cover.Id, "POST");
        return Ok(cover);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        _auditer.AuditCover(id, "DELETE");
        await _claimsContext.DeleteCoverAsync(id);
    }
}
