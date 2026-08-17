using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor beschikbaarheidsbeheer van Vrijwilligers
/// </summary>
public interface IBeschikbaarheidRepository
{
    Task<IEnumerable<Beschikbaarheid>> GetForGebruikerAsync(string gebruikerId);
    Task AddAsync(Beschikbaarheid beschikbaarheid);
    Task DeleteAsync(int id);
    Task DeleteAllForGebruikerAsync(string gebruikerId);
}
