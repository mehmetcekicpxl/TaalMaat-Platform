using TaalMaat.Core.Enums;

namespace TaalMaat.Core.Entities;

/// <summary>
/// Een buddyverzoek van een Anderstalige naar een Vrijwilliger.
/// Maximum 3 actieve buddies per Vrijwilliger .
/// </summary>
public class BuddyVerzoek
{
    public int Id { get; set; }

    // Verzender is altijd een Anderstalige
    public string VerzenderId { get; set; } = string.Empty;
    public ApplicationUser Verzender { get; set; } = null!;

    // Ontvanger is altijd een Vrijwilliger
    public string OntvangerId { get; set; } = string.Empty;
    public ApplicationUser Ontvanger { get; set; } = null!;

    // Status van het verzoek
    public BuddyStatus Status { get; set; } = BuddyStatus.Wachtend;

    // Tijdstip van aanmaak
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;

    // Optionele afwijzingsboodschap
    public string? AfwijzingBericht { get; set; }
}
