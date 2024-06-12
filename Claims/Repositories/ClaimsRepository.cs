using Claims.Auditing;
using Claims.Controllers;

namespace Claims.Repositories;

public class ClaimsRepository : IClaimsRepository
{
    private readonly ClaimsContext _context;
    private readonly Auditer _auditer;

    public ClaimsRepository(ClaimsContext context, AuditContext auditContext)
    {
        _context = context;
        _auditer = new Auditer(auditContext);
    }

    public async Task<IEnumerable<Claim>> GetAllAsync()
    {
        return await _context.GetAllClaimsAsync();
    }

    public async Task<Claim?> GetAsync(string id)
    {
        return await _context.GetClaimAsync(id);
    }

    public async Task<Claim> CreateAsync(Claim claim)
    {
        claim.Id = Guid.NewGuid().ToString();
        await _context.AddClaimAsync(claim);
        await _auditer.AuditClaim(claim.Id, "POST");
        return claim;
    }

    public async Task DeleteAsync(string id)
    {
        await _auditer.AuditClaim(id, "DELETE");
        await _context.DeleteClaimAsync(id);
    }
}
