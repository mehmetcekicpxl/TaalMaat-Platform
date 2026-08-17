using System.ComponentModel.DataAnnotations;

namespace TaalMaat.Core.Entities;

/// <summary>
/// Externe oefenbron of nuttige link die door een admin wordt toegevoegd.
/// Wordt getoond op de zelfstudiepagina als aanvullend materiaal.
/// </summary>
public class ExterneBron
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titel { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Beschrijving { get; set; }

    public DateTime ToegevoegdOp { get; set; } = DateTime.UtcNow;
}
