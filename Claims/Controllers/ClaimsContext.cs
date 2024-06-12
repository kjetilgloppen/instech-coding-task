using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Claims.Controllers;

public class ClaimsContext : DbContext
{
    private DbSet<Claim> Claims { get; init; }
    public DbSet<Cover>  Covers { get; init; }

    public ClaimsContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Claim>().ToCollection("claims");
        modelBuilder.Entity<Cover>().ToCollection("covers");
    }

    public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
    {
        return await Claims.ToListAsync();
    }

    public async Task<Claim?> GetClaimAsync(string id)
    {
        return await Claims
            .Where(claim => claim.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task AddClaimAsync(Claim item)
    {
        Claims.Add(item);
        await SaveChangesAsync();
    }

    public async Task DeleteClaimAsync(string id)
    {
        var claim = await GetClaimAsync(id);
        if (claim is not null)
        {
            Claims.Remove(claim);
            await SaveChangesAsync();
        }
    }
}
