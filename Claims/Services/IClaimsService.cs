namespace Claims.Services;

public interface IClaimsService
{
    public Task<IEnumerable<Claim>> GetAllAsync();
    public Task<Claim> GetAsync(string id);
    public Task<Claim> CreateAsync(Claim claim);
    public Task DeleteAsync(string id);
}
