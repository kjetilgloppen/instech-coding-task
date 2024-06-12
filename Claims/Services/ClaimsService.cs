using Claims.Repositories;

namespace Claims.Services;

public class ClaimsService
{
    private readonly ClaimsRepository _claimsRepository;

    public ClaimsService(ClaimsRepository claimsRepository)
    {
        _claimsRepository = claimsRepository;
    }

    public async Task<IEnumerable<Claim>> GetAllAsync()
    {
        return await _claimsRepository.GetAllAsync();
    }

    public async Task<Claim?> GetAsync(string id)
    {
        return await _claimsRepository.GetAsync(id);
    }

    public async Task<Claim> CreateAsync(Claim claim)
    {
        await _claimsRepository.CreateAsync(claim);
        return claim;
    }

    public async Task DeleteAsync(string id)
    {
        await _claimsRepository.DeleteAsync(id);
    }
}
