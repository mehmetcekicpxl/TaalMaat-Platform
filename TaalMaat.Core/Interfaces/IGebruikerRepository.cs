using TaalMaat.Core.Entities;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor gebruikersbeheer
/// </summary>
public interface IGebruikerRepository
{
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task<IEnumerable<ApplicationUser>> GetByRolAsync(Enums.GebruikerRol rol);
    Task UpdateAsync(ApplicationUser gebruiker);
    Task DeleteAsync(string id);
}
