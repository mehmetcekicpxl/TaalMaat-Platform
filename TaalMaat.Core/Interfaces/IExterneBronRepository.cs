using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor externe oefenbronnen (links naar Nedbox, etc.)
/// </summary>
public interface IExterneBronRepository
{
    Task<IEnumerable<ExterneBron>> GetAllAsync();
    Task<ExterneBron?> GetByIdAsync(int id);
    Task AddAsync(ExterneBron bron);
    Task UpdateAsync(ExterneBron bron);
    Task DeleteAsync(int id);
}
