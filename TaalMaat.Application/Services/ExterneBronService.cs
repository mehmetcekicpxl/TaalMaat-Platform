using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor het beheren van externe oefenbronnen (links naar Nedbox, etc.)
/// </summary>
public class ExterneBronService
{
    private readonly IExterneBronRepository _repo;

    public ExterneBronService(IExterneBronRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ExterneBron>> GetAllAsync() =>
        await _repo.GetAllAsync();

    public async Task<ExterneBron?> GetByIdAsync(int id) =>
        await _repo.GetByIdAsync(id);

    public async Task ToevoegenAsync(ExterneBron bron) =>
        await _repo.AddAsync(bron);

    public async Task UpdateAsync(ExterneBron bron) =>
        await _repo.UpdateAsync(bron);

    public async Task VerwijderAsync(int id) =>
        await _repo.DeleteAsync(id);
}
