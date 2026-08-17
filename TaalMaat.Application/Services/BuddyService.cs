using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor het buddysysteem. Handhaven maximaal 3 actieve buddies per Vrijwilliger.
/// </summary>
public class BuddyService
{
    private readonly IBuddyRepository _buddyRepo;
    private readonly INotificatieService _notificatieService;

    private const int MaxActieveBuddies = 3;

    public BuddyService(IBuddyRepository buddyRepo, INotificatieService notificatieService)
    {
        _buddyRepo = buddyRepo;
        _notificatieService = notificatieService;
    }

    /// <summary>
    /// Stuurt een buddyverzoek van een Anderstalige naar een Vrijwilliger.
    /// Stuurt ook een realtime SignalR-melding via INotificatieService.
    /// </summary>
    public async Task<(bool Succes, string Bericht)> StuurVerzoekAsync(string verzenderId, string ontvangerId, ApplicationUser verzender)
    {
        // 1. Controleer of ze al actieve buddies zijn (Geaccepteerd)
        var bestaandeKoppeling = await _buddyRepo.GetKoppelingAsync(ontvangerId, verzenderId);
        if (bestaandeKoppeling != null && bestaandeKoppeling.IsActief)
            return (false, "U bent al verbonden met deze Vrijwilliger.");

        // 2. Controleer of er al een openstaand verzoek bestaat.
        
        var bestaandeVerzoeken = await _buddyRepo.GetVerzoekForVerzenderAsync(verzenderId);
        if (bestaandeVerzoeken.Any(v => v.OntvangerId == ontvangerId && v.Status == BuddyStatus.Wachtend))
            return (false, "Er is al een openstaand verzoek naar deze Vrijwilliger.");

        // 3. Controleer max. 3 buddies van de Vrijwilliger
        var aantalBuddies = await _buddyRepo.GetAantalActieveBuddiesAsync(ontvangerId);
        if (aantalBuddies >= MaxActieveBuddies)
            return (false, "Deze Vrijwilliger heeft al het maximum aantal buddies bereikt.");

        var verzoek = new BuddyVerzoek
        {
            VerzenderId = verzenderId,
            OntvangerId = ontvangerId,
            Status = BuddyStatus.Wachtend,
            AangemaaktOp = DateTime.UtcNow
        };

        await _buddyRepo.AddVerzoekAsync(verzoek);

        // Stuur realtime melding naar de Vrijwilliger via de notificatieservice
        await _notificatieService.StuurNieuwBuddyVerzoekAsync(ontvangerId, new
        {
            VerzenderId = verzenderId,
            VerzenderNaam = verzender.UserName,
            Hobbies = verzender.Hobbies,
            ShortBio = verzender.ShortBio,
            VerzoekId = verzoek.Id
        });

        return (true, "Buddyverzoek succesvol verzonden.");
    }

    /// <summary>
    /// Accepteer een buddyverzoek en maak een actieve koppeling aan.
    /// </summary>
    public async Task<(bool Succes, string Bericht)> AccepteerVerzoekAsync(int verzoekId)
    {
        var verzoek = await _buddyRepo.GetVerzoekByIdAsync(verzoekId);
        if (verzoek == null) return (false, "Verzoek niet gevonden.");

        // Hercontroleer maximumlimiet
        var aantalBuddies = await _buddyRepo.GetAantalActieveBuddiesAsync(verzoek.OntvangerId);
        if (aantalBuddies >= MaxActieveBuddies)
            return (false, "Maximum aantal buddies bereikt.");

        verzoek.Status = BuddyStatus.Geaccepteerd;
        await _buddyRepo.UpdateVerzoekAsync(verzoek);

        var koppeling = new BuddyKoppeling
        {
            VrijwilligerId = verzoek.OntvangerId,
            AnderstaligId = verzoek.VerzenderId,
            GekoppeldOp = DateTime.UtcNow,
            IsActief = true
        };

        await _buddyRepo.AddKoppelingAsync(koppeling);

        // Melding naar Anderstalige sturen
        await _notificatieService.StuurBuddyVerzoekGeaccepteerdAsync(verzoek.VerzenderId, new { VerzoekId = verzoekId });

        return (true, "Buddyverzoek geaccepteerd!");
    }

    /// <summary>
    /// Wijs een buddyverzoek af met een beleefde boodschap.
    /// </summary>
    public async Task<(bool Succes, string Bericht)> WijsAfAsync(int verzoekId)
    {
        var verzoek = await _buddyRepo.GetVerzoekByIdAsync(verzoekId);
        if (verzoek == null) return (false, "Verzoek niet gevonden.");

        verzoek.Status = BuddyStatus.Afgewezen;
        // Beleefde, zachte afwijzing
        verzoek.AfwijzingBericht = "Bedankt voor je interesse! Deze vrijwilliger heeft momenteel helaas al voldoende actieve buddy's om te begeleiden. Probeer gerust een andere vrijwilliger uit de lijst, ze helpen je graag verder!";
        await _buddyRepo.UpdateVerzoekAsync(verzoek);

        // Melding naar Anderstalige sturen
        await _notificatieService.StuurBuddyVerzoekAfgewezenAsync(verzoek.VerzenderId, new
        {
            VerzoekId = verzoekId,
            Bericht = verzoek.AfwijzingBericht
        });

        return (true, "Verzoek afgewezen.");
    }

    /// <summary>
    /// Silent Unmatch: verwijdert een actieve koppeling.
    /// Als de Vrijwilliger daarna minder dan 3 buddies heeft, verschijnt hij weer in de lijst.
    /// </summary>
    public async Task<bool> OntkoppelAsync(string vrijwilligerId, string anderstaligId)
    {
        bool anyDeactivated = false;

       
        while (true)
        {
            var koppeling = await _buddyRepo.GetKoppelingAsync(vrijwilligerId, anderstaligId);
            if (koppeling == null)
                break; 

            koppeling.IsActief = false;
            await _buddyRepo.UpdateKoppelingAsync(koppeling);
            anyDeactivated = true;
        }

        return anyDeactivated;
    }

    public async Task<IEnumerable<BuddyVerzoek>> GetInkomendeVerzoek(string ontvangerId) =>
        await _buddyRepo.GetVerzoekForOntvangerAsync(ontvangerId);

    public async Task<IEnumerable<BuddyVerzoek>> GetAfgewezenVerzoekenVoorVerzenderAsync(string verzenderId)
    {
        var verzoeken = await _buddyRepo.GetVerzoekForVerzenderAsync(verzenderId);
        return verzoeken.Where(v => v.Status == BuddyStatus.Afgewezen);
    }

    public async Task<IEnumerable<BuddyVerzoek>> GetGeaccepteerdeVerzoekenVoorVerzenderAsync(string verzenderId)
    {
        var verzoeken = await _buddyRepo.GetVerzoekForVerzenderAsync(verzenderId);
        return verzoeken.Where(v => v.Status == BuddyStatus.Geaccepteerd);
    }

    public async Task VerwijderVerzoekAsync(int verzoekId)
    {
        await _buddyRepo.DeleteVerzoekAsync(verzoekId);
    }

    public async Task<IEnumerable<BuddyKoppeling>> GetActieveBuddiesVanVrijwilliger(string vrijwilligerId) =>
        await _buddyRepo.GetActieveKoppelingenForVrijwilligerAsync(vrijwilligerId);

    public async Task<IEnumerable<BuddyKoppeling>> GetActieveBuddiesVanAnderstalig(string anderstaligId) =>
        await _buddyRepo.GetActieveKoppelingenForAnderstaligAsync(anderstaligId);

    /// <summary>
    /// Controleert of een Vrijwilliger zichtbaar is in de zoeklijst (minder dan 3 actieve buddies)
    /// </summary>
    public async Task<bool> IsZichtbaarInLijstAsync(string vrijwilligerId)
    {
        var aantalBuddies = await _buddyRepo.GetAantalActieveBuddiesAsync(vrijwilligerId);
        return aantalBuddies < MaxActieveBuddies;
    }
}
