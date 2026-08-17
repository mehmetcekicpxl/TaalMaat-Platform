using TaalMaat.Core.Enums;

namespace TaalMaat.Core.Entities;

/// <summary>
/// Een taaloefening (tekst of YouTube-video) voor Anderstaligen.
/// Moet goedgekeurd worden door Admin voor publicatie.
/// </summary>
public class Oefening
{
    public int Id { get; set; }

    public string Titel { get; set; } = string.Empty;

    // Leestekst (null als YouTube-video wordt gebruikt)
    public string? Inhoud { get; set; }

    // YouTube embed-URL (null als leestekst of audio wordt gebruikt)
    public string? YouTubeUrl { get; set; }

    // Audio-URL voor luisteroefeningen (null als tekst of video wordt gebruikt)
    public string? AudioUrl { get; set; }

    // Taalniveau A1 t/m C2
    public OefeningNiveau Niveau { get; set; }

    // Admin moet oefening goedkeuren
    public bool IsGoedgekeurd { get; set; } = false;

    // Tijdstip van aanmaak
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;

    // Navigatie: bijbehorende meerkeuzevragen
    public ICollection<OefeningVraag> Vragen { get; set; } = new List<OefeningVraag>();
}
