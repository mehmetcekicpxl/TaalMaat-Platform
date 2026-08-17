using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor het plannen en beheren van videosessies.
/// Genereert automatisch een unieke Jitsi-vergader-URL.
/// </summary>
public class SessieService
{
    private readonly ISessieRepository _sessieRepo;
    private readonly JitsiService _jitsiService;

    public SessieService(ISessieRepository sessieRepo, JitsiService jitsiService)
    {
        _sessieRepo = sessieRepo;
        _jitsiService = jitsiService;
    }

    /// <summary>
    /// Plant een nieuwe sessie en genereert automatisch een Jitsi-Video-URL
    /// </summary>
    public async Task<Sessie> PlanSessieAsync(string vrijwilligerId, string anderstaligId, DateTime geplandOp)
    {
        var sessie = new Sessie
        {
            VrijwilligerId = vrijwilligerId,
            AnderstaligId = anderstaligId,
            GeplandOp = geplandOp,
            JitsiUrl = _jitsiService.GenereerUrl(),
            IsBevestigd = false,
            AangemaaktOp = DateTime.UtcNow
        };

        await _sessieRepo.AddAsync(sessie);
        return sessie;
    }

    public async Task<IEnumerable<Sessie>> GetSessiesVanVrijwilligerAsync(string vrijwilligerId) =>
        await _sessieRepo.GetForVrijwilligerAsync(vrijwilligerId);

    public async Task<IEnumerable<Sessie>> GetSessiesVanAnderstaligAsync(string anderstaligId) =>
        await _sessieRepo.GetForAnderstaligAsync(anderstaligId);

    public async Task BevestigSessieAsync(int sessieId)
    {
        var sessie = await _sessieRepo.GetByIdAsync(sessieId);
        if (sessie != null)
        {
            sessie.IsBevestigd = true;
            await _sessieRepo.UpdateAsync(sessie);
        }
    }

    public async Task VerwijderSessieAsync(int sessieId) =>
        await _sessieRepo.DeleteAsync(sessieId);
}
