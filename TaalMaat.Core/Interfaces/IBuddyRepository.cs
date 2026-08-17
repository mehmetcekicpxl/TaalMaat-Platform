using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor buddyverzoeken en koppelingen
/// </summary>
public interface IBuddyRepository
{
    Task<BuddyVerzoek?> GetVerzoekByIdAsync(int id);
    Task<IEnumerable<BuddyVerzoek>> GetVerzoekForOntvangerAsync(string ontvangerId);
    Task<IEnumerable<BuddyVerzoek>> GetVerzoekForVerzenderAsync(string verzenderId);
    Task AddVerzoekAsync(BuddyVerzoek verzoek);
    Task UpdateVerzoekAsync(BuddyVerzoek verzoek);
    Task DeleteVerzoekAsync(int verzoekId);

    // Koppeling beheer
    Task<IEnumerable<BuddyKoppeling>> GetActieveKoppelingenForVrijwilligerAsync(string vrijwilligerId);
    Task<IEnumerable<BuddyKoppeling>> GetActieveKoppelingenForAnderstaligAsync(string anderstaligId);
    Task AddKoppelingAsync(BuddyKoppeling koppeling);
    Task<BuddyKoppeling?> GetKoppelingAsync(string vrijwilligerId, string anderstaligId);
    Task UpdateKoppelingAsync(BuddyKoppeling koppeling);

    /// Telt het aantal actieve buddies van een Vrijwilliger (max. 3)
    Task<int> GetAantalActieveBuddiesAsync(string vrijwilligerId);
}
