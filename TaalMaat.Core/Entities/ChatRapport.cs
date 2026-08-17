using System.ComponentModel.DataAnnotations;

namespace TaalMaat.Core.Entities;

public class ChatRapport
{
    public int Id { get; set; }

    [Required]
    public string RapporteerderId { get; set; } = string.Empty;
    public ApplicationUser? Rapporteerder { get; set; }

    [Required]
    public string GerapporteerdeId { get; set; } = string.Empty;
    public ApplicationUser? Gerapporteerde { get; set; }

    public DateTime RapportageDatum { get; set; } = DateTime.UtcNow;

    public string? Toelichting { get; set; }

    public bool IsAfgehandeld { get; set; } = false;

    [Required]
    public bool ToestemmingGegeven { get; set; } = false;
}
