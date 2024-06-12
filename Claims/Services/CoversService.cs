using Claims.Helpers;
using Claims.Repositories;

namespace Claims.Services;

public class CoversService
{
    private readonly CoversRepository _coversRepository;

    public CoversService(CoversRepository coversRepository)
    {
        _coversRepository = coversRepository;
    }

    public async Task<IEnumerable<Cover>> GetAllAsync()
    {
        return await _coversRepository.GetAllAsync();
    }

    public async Task<Cover?> GetAsync(string id)
    {
        return await _coversRepository.GetAsync(id);
    }

    public async Task<Cover> CreateAsync(Cover cover)
    {
        cover.Premium = CoversHelper.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _coversRepository.CreateAsync(cover);
        return cover;
    }

    public async Task DeleteAsync(string id)
    {
        await _coversRepository.DeleteAsync(id);
    }
}
