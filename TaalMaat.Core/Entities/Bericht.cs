using System.ComponentModel.DataAnnotations;

namespace TaalMaat.Core.Entities;

/// <summary>
/// Representeert een tekstbericht verstuurd tussen een Vrijwilliger en een Anderstalige.
/// </summary>
public class Bericht
{
    public int Id { get; set; }

    [Required]
    public string AfzenderId { get; set; } = string.Empty;
    public ApplicationUser? Afzender { get; set; }

    [Required]
    public string OntvangerId { get; set; } = string.Empty;
    public ApplicationUser? Ontvanger { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Inhoud { get; set; } = string.Empty;

    public DateTime VerzondenOp { get; set; } = DateTime.UtcNow;

    public bool IsGelezen { get; set; } = false;
}
