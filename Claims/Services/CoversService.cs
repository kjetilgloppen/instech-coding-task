using Claims.Helpers;
using Claims.Repositories;
using Claims.Validators;

namespace Claims.Services;

public class CoversService : ICoversService
{
    private readonly ICoversRepository _coversRepository;

    public CoversService(ICoversRepository coversRepository)
    {
        _coversRepository = coversRepository;
    }

    public async Task<IEnumerable<Cover>> GetAllAsync()
    {
        return await _coversRepository.GetAllAsync();
    }

    public async Task<Cover> GetAsync(string id)
    {
        return await _coversRepository.GetAsync(id);
    }

    public async Task<Cover> CreateAsync(Cover cover)
    {
        CoverValidator.Validate(cover);
        cover.Premium = CoversHelper.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _coversRepository.CreateAsync(cover);
        return cover;
    }

    public async Task DeleteAsync(string id)
    {
        await _coversRepository.DeleteAsync(id);
    }
}
