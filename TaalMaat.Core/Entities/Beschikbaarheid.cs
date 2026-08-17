namespace TaalMaat.Core.Entities;

/// <summary>
/// Beschikbaarheid van een Vrijwilliger per dag en tijdslot
/// </summary>
public class Beschikbaarheid
{
    public int Id { get; set; }

    public string GebruikerId { get; set; } = string.Empty;
    public ApplicationUser Gebruiker { get; set; } = null!;

    // Dag van de week (0 = zondag, 1 = maandag, ... 6 = zaterdag)
    public DayOfWeek DagVanDeWeek { get; set; }

    // Beschikbaarheidstijdslot
    public TimeOnly StartTijd { get; set; }
    public TimeOnly EindTijd { get; set; }
}
