namespace Claims.Services;

public interface ICoversService
{
    public Task<IEnumerable<Cover>> GetAllAsync();
    public Task<Cover?> GetAsync(string id);
    public Task<Cover> CreateAsync(Cover cover);
    public Task DeleteAsync(string id);
}
