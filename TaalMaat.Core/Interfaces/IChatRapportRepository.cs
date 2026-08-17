using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

public interface IChatRapportRepository
{
    Task AddRapportAsync(ChatRapport rapport);
    Task<IEnumerable<ChatRapport>> GetRapportenAsync();
    Task<ChatRapport?> GetByIdAsync(int id);
    Task UpdateAsync(ChatRapport rapport);
}
