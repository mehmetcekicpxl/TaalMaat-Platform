using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor het beheren van de chatberichten (BuddyChat).
/// </summary>
public interface IBerichtRepository
{
    Task<IEnumerable<Bericht>> GetChatGeschiedenisAsync(string gebruikerAId, string gebruikerBId);
    Task AddBerichtAsync(Bericht bericht);
    Task MarkeerAlsGelezenAsync(string afzenderId, string ontvangerId);
    Task<int> GetAantalOngelezenBerichtenAsync(string ontvangerId);
    Task<int> GetAantalOngelezenBerichtenVanAfzenderAsync(string ontvangerId, string afzenderId);
    Task<IEnumerable<Bericht>> GetBerichtenTussenGebruikersAsync(string userAId, string userBId, DateTime? totDatum = null);
}
