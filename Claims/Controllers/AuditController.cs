
using Claims.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class AuditController : ControllerBase
{
    private readonly AuditContext _auditContext;

    public AuditController(AuditContext auditContext)
    {
        _auditContext = auditContext;
    }

    [HttpGet("claims")]
    public async Task<IEnumerable<ClaimAudit>> GetAllClaimAuditsAsync()
    {
        return await _auditContext.GetAllClaimAudits();
    }

    [HttpGet("covers")]
    public async Task<IEnumerable<CoverAudit>> GetAllCoverAuditsAsync()
    {
        return await _auditContext.GetAllCoverAudits();
    }
}
