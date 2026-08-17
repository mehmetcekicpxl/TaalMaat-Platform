namespace TaalMaat.Core.Entities;

/// <summary>
/// Een meerkeuzevraag behorende bij een taaloefening.
/// Opties worden opgeslagen als JSON-array in de database.
/// </summary>
public class OefeningVraag
{
    public int Id { get; set; }

    public int OefeningId { get; set; }
    public Oefening Oefening { get; set; } = null!;

    public string VraagTekst { get; set; } = string.Empty;

    // Juist antwoord (moet overeenkomen met één van de opties)
    public string JuistAntwoord { get; set; } = string.Empty;

    // Opties als JSON-array (bijv. ["A", "B", "C", "D"])
    public string OptiesJson { get; set; } = "[]";
}
