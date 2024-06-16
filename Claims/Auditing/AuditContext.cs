using Microsoft.EntityFrameworkCore;

namespace Claims.Auditing;

public class AuditContext : DbContext
{
    public DbSet<ClaimAudit> ClaimAudits { get; set; }
    public DbSet<CoverAudit> CoverAudits { get; set; }

    public AuditContext(DbContextOptions<AuditContext> options) : base(options)
    {
    }

    public async Task<IEnumerable<ClaimAudit>> GetAllClaimAudits()
    {
        return await ClaimAudits.ToListAsync();
    }

    public async Task<IEnumerable<CoverAudit>> GetAllCoverAudits()
    {
        return await CoverAudits.ToListAsync();
    }
}
