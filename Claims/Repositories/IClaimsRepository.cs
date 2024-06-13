namespace Claims.Repositories;

public interface IClaimsRepository
{
    public Task<IEnumerable<Claim>> GetAllAsync();
    public Task<Claim> GetAsync(string id);
    public Task<Claim> CreateAsync(Claim claim);
    public Task DeleteAsync(string id);
}
