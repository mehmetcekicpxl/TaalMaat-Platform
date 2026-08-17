namespace TaalMaat.Core.Entities;

/// <summary>
/// Gemeente voor multi-tenancy voorbereiding
/// </summary>
public class Gemeente
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;

    // Navigatie-eigenschap
    public ICollection<ApplicationUser> Gebruikers { get; set; } = new List<ApplicationUser>();
}
