namespace TaalMaat.Application.Services;

/// <summary>
/// Genereert unieke Jitsi Meet-video-URL's voor TaalMaat-sessies.
/// </summary>
public class JitsiService
{
    /// <summary>
    /// Genereert een unieke Jitsi-kamer-URL in het formaat: meet.jit.si/TaalMaat-{Guid}
    /// </summary>
    public string GenereerUrl()
    {
        var kamerNaam = $"TaalMaat-{Guid.NewGuid():N}";
        return $"https://meet.ffmuc.net/{kamerNaam}";
    }

    /// <summary>
    /// Controleert of de knop actief moet zijn op basis van de geplande tijd en de configuratie
    /// </summary>
    public bool IsVideoButtonActief(DateTime geplandOp, int aanloopMinuten)
    {
        var nu = DateTime.UtcNow;
        var activatietijd = geplandOp.AddMinutes(-aanloopMinuten);
        return nu >= activatietijd && nu <= geplandOp.AddHours(2);
    }
}
