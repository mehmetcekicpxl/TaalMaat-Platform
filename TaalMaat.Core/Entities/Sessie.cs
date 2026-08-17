namespace TaalMaat.Core.Entities;

/// <summary>
/// Een geplande sessie (videogesprek) tussen een Vrijwilliger en Anderstalige.
/// De Jitsi-link wordt automatisch gegenereerd.
/// </summary>
public class Sessie
{
    public int Id { get; set; }

    public string VrijwilligerId { get; set; } = string.Empty;
    public ApplicationUser Vrijwilliger { get; set; } = null!;

    public string AnderstaligId { get; set; } = string.Empty;
    public ApplicationUser Anderstalig { get; set; } = null!;

    // Geplande datum en tijd
    public DateTime GeplandOp { get; set; }

    // Unieke Jitsi-vergader-URL (bijv. meet.jit.si/TaalMaat-{Guid})
    public string JitsiUrl { get; set; } = string.Empty;

    // Is de sessie bevestigd door beide partijen?
    public bool IsBevestigd { get; set; } = false;

    // Tijdstip van aanmaak
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;
}
