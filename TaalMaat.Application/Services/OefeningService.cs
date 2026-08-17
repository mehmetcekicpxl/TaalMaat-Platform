using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor de zelfstudiemodule: beheer van taaloefeningen
/// </summary>
public class OefeningService
{
    private readonly IOefeningRepository _oefeningRepo;

    public OefeningService(IOefeningRepository oefeningRepo)
    {
        _oefeningRepo = oefeningRepo;
    }

    public async Task<IEnumerable<Oefening>> GetGoedgekeurdeByNiveauAsync(OefeningNiveau niveau) =>
        await _oefeningRepo.GetGoedgekeurdeByNiveauAsync(niveau);

    public async Task<Oefening?> GetByIdAsync(int id) =>
        await _oefeningRepo.GetByIdAsync(id);

    public async Task<IEnumerable<Oefening>> GetAllAsync() =>
        await _oefeningRepo.GetAllAsync();

    public async Task MaakOefeningAanAsync(Oefening oefening) =>
        await _oefeningRepo.AddAsync(oefening);

    public async Task UpdateOefeningAsync(Oefening oefening) =>
        await _oefeningRepo.UpdateAsync(oefening);

    public async Task VerwijderOefeningAsync(int id) =>
        await _oefeningRepo.DeleteAsync(id);

    /// <summary>
    /// Keurt een oefening goed (zet IsGoedgekeurd = true)
    /// </summary>
    public async Task KeurOefeningGoedAsync(int id)
    {
        var oefening = await _oefeningRepo.GetByIdAsync(id);
        if (oefening != null)
        {
            oefening.IsGoedgekeurd = true;
            await _oefeningRepo.UpdateAsync(oefening);
        }
    }
}
