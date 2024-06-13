using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Claims.Controllers;

public class ClaimsContext : DbContext
{
    private DbSet<Claim> Claims { get; init; }
    private DbSet<Cover>  Covers { get; init; }

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

    public async Task<Claim> GetClaimAsync(string id)
    {
        var claim = await Claims
            .Where(claim => claim.Id == id)
            .SingleOrDefaultAsync();

        return claim ?? throw new KeyNotFoundException("Claim not found");
    }

    public async Task AddClaimAsync(Claim item)
    {
        Claims.Add(item);
        await SaveChangesAsync();
    }

    public async Task DeleteClaimAsync(string id)
    {
        var claim = await GetClaimAsync(id);

        Claims.Remove(claim);
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<Cover>> GetAllCoversAsync()
    {
        return await Covers.ToListAsync();
    }

    public async Task<Cover?> GetCoverAsync(string id)
    {
        return await Covers
            .Where(cover => cover.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task AddCoverAsync(Cover item)
    {
        Covers.Add(item);
        await SaveChangesAsync();
    }

    public async Task DeleteCoverAsync(string id)
    {
        var cover = await GetCoverAsync(id);
        if (cover is not null)
        {
            Covers.Remove(cover);
            await SaveChangesAsync();
        }
    }
}
