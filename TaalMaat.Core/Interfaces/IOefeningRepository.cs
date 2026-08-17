using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;

namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Repository-interface voor taaloefeningen (zelfstudiemodule)
/// </summary>
public interface IOefeningRepository
{
    Task<Oefening?> GetByIdAsync(int id);
    Task<IEnumerable<Oefening>> GetGoedgekeurdeByNiveauAsync(OefeningNiveau niveau);
    Task<IEnumerable<Oefening>> GetAllAsync(); // voor Admin
    Task AddAsync(Oefening oefening);
    Task UpdateAsync(Oefening oefening);
    Task DeleteAsync(int id);
}
