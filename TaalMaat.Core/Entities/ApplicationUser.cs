using Microsoft.AspNetCore.Identity;
using TaalMaat.Core.Enums;

namespace TaalMaat.Core.Entities;

/// <summary>
/// Uitbreiding van IdentityUser met TaalMaat-specifieke profielvelden.
/// E-mail en telefoonnummer zijn STRIKT VERBORGEN voor andere gebruikers.
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Contactgegevens – alleen zichtbaar voor eigenaar en Admin
    public string? ContactPhoneNumber { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MaxLength(50)]
    public string Voornaam { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MaxLength(50)]
    public string Achternaam { get; set; } = string.Empty;

    public string VolledigeNaam => $"{Voornaam} {Achternaam}";

    // Uitgebreid profiel
    public string? Hobbies { get; set; }
    public string? ShortBio { get; set; }

    // Rol binnen het platform
    public GebruikerRol Rol { get; set; } = GebruikerRol.Anderstalig;

    // Vrijwilliger moet goedgekeurd worden door Admin
    public bool IsGeaccepteerd { get; set; } = false;

    // Vrijwilliger wachtkamer status
    public bool HeeftWachtkamerGezien { get; set; } = false;
    public bool IsAfgekeurd { get; set; } = false;

    // Multi-tenancy: gemeente van de gebruiker
    public int? GemeenteId { get; set; }
    public Gemeente? Gemeente { get; set; }

    // GDPR: gebruiker heeft de voorwaarden aanvaard
    public bool AccepteertVoorwaarden { get; set; } = false;

    // Tijdstip van registratie
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;

    // Is het account actief (niet geschorst)
    public bool IsActief { get; set; } = true;

    // Geheim woord voor wachtwoord-reset verificatie (2-staps beveiliging)
    public string? GeheimWoord { get; set; }

    // Navigatie-eigenschap voor beschikbaarheden (1-op-veel)
    public ICollection<Beschikbaarheid> Beschikbaarheden { get; set; } = new List<Beschikbaarheid>();
    // Navigatie-eigenschappen voor buddyverzoeken (1-op-veel)
    public ICollection<BuddyVerzoek> VerzondBuddyVerzoeken { get; set; } = new List<BuddyVerzoek>();
    // Navigatie-eigenschappen voor ontvangen buddyverzoeken (1-op-veel)
    public ICollection<BuddyVerzoek> OntvangenBuddyVerzoeken { get; set; } = new List<BuddyVerzoek>();


}
