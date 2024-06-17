using System.Collections.Concurrent;
using Timer = System.Timers.Timer;

namespace Claims.Auditing;

public class Auditer
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ConcurrentQueue<object> _queue = new();
    private readonly Timer _timer;

    public Auditer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _timer = new Timer
        {
            Interval = 1000,
            Enabled = true,
            AutoReset = false,
        };
        _timer.Elapsed += (_, _) => SaveAuditsToDb();
    }

    public void AuditClaim(string id, string httpRequestType)
    {
        var claimAudit = new ClaimAudit
        {
            Created = DateTime.Now,
            HttpRequestType = httpRequestType,
            ClaimId = id,
        };

        _queue.Enqueue(claimAudit);
    }
        
    public void AuditCover(string id, string httpRequestType)
    {
        var coverAudit = new CoverAudit
        {
            Created = DateTime.Now,
            HttpRequestType = httpRequestType,
            CoverId = id,
        };

        _queue.Enqueue(coverAudit);
    }

    private void SaveAuditsToDb()
    {
        while (_queue.TryDequeue(out var item))
        {
            SaveToDb(item);
        }
        // Get done with saving to DB before triggering the timer again
        _timer.Start();
    }

    private void SaveToDb(object entity)
    {
        using var scope = _scopeFactory.CreateScope();
        var auditContext = scope.ServiceProvider.GetRequiredService<AuditContext>();

        auditContext.Add(entity);
        auditContext.SaveChanges();
    }
}
