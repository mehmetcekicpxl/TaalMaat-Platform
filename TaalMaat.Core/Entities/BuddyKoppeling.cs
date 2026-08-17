namespace TaalMaat.Core.Entities;

/// <summary>
/// Actieve koppeling tussen een Vrijwilliger en een Anderstalige (N:N).
/// Maximaal 3 actieve koppelingen per Vrijwilliger.
/// </summary>
public class BuddyKoppeling
{
    public int Id { get; set; }

    public string VrijwilligerId { get; set; } = string.Empty;
    public ApplicationUser Vrijwilliger { get; set; } = null!;

    public string AnderstaligId { get; set; } = string.Empty;
    public ApplicationUser Anderstalig { get; set; } = null!;

    // Wanneer de koppeling tot stand is gekomen
    public DateTime GekoppeldOp { get; set; } = DateTime.UtcNow;

    // Actief of Silent Unmatch
    public bool IsActief { get; set; } = true;
}
