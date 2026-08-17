using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor videosessies tussen Vrijwilliger en Anderstalige
/// </summary>
public interface ISessieRepository
{
    Task<Sessie?> GetByIdAsync(int id);
    Task<IEnumerable<Sessie>> GetForVrijwilligerAsync(string vrijwilligerId);
    Task<IEnumerable<Sessie>> GetForAnderstaligAsync(string anderstaligId);
    Task AddAsync(Sessie sessie);
    Task UpdateAsync(Sessie sessie);
    Task DeleteAsync(int id);
}
