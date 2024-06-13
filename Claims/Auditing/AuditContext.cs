using Microsoft.EntityFrameworkCore;

namespace Claims.Auditing;

public class AuditContext : DbContext
{
    public DbSet<ClaimAudit> ClaimAudits { get; set; }
    public DbSet<CoverAudit> CoverAudits { get; set; }

    public AuditContext(DbContextOptions<AuditContext> options) : base(options)
    {
    }
}
