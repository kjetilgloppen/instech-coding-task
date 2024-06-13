using Claims.Auditing;
using Claims.Controllers;

namespace Claims.Repositories;

public class CoversRepository : ICoversRepository
{
    private readonly ClaimsContext _context;
    private readonly Auditer _auditer;

    public CoversRepository(ClaimsContext context, AuditContext auditContext)
    {
        _context = context;
        _auditer = new Auditer(auditContext);
    }

    public async Task<IEnumerable<Cover>> GetAllAsync()
    {
        return await _context.GetAllCoversAsync();
    }

    public async Task<Cover> GetAsync(string id)
    {
        return await _context.GetCoverAsync(id);
    }

    public async Task<Cover> CreateAsync(Cover cover)
    {
        cover.Id = Guid.NewGuid().ToString();
        await _context.AddCoverAsync(cover);
        await _auditer.AuditCover(cover.Id, "POST");
        return cover;
    }

    public async Task DeleteAsync(string id)
    {
        await _auditer.AuditCover(id, "DELETE");
        await _context.DeleteCoverAsync(id);
    }
}
