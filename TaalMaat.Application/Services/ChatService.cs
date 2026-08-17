using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor het verzenden, ontvangen en beheren van realtime tekstberichten (BuddyChat).
/// </summary>
public class ChatService
{
    private readonly IBerichtRepository _berichtRepo;
    private readonly IBuddyRepository _buddyRepo;
    private readonly INotificatieService _notificatieService;
    private readonly ChatEventService _chatEventService;
    private readonly EncryptieService _encryptieService;
    private readonly IChatRapportRepository _chatRapportRepo;

    public ChatService(
        IBerichtRepository berichtRepo, 
        IBuddyRepository buddyRepo, 
        INotificatieService notificatieService, 
        ChatEventService chatEventService, 
        EncryptieService encryptieService,
        IChatRapportRepository chatRapportRepo)
    {
        _berichtRepo = berichtRepo;
        _buddyRepo = buddyRepo;
        _notificatieService = notificatieService;
        _chatEventService = chatEventService;
        _encryptieService = encryptieService;
        _chatRapportRepo = chatRapportRepo;
    }

    public async Task<IEnumerable<Bericht>> GetChatGeschiedenisAsync(string gebruikerAId, string gebruikerBId)
    {
        var geschiedenis = await _berichtRepo.GetChatGeschiedenisAsync(gebruikerAId, gebruikerBId);
        foreach (var b in geschiedenis)
        {
            b.Inhoud = _encryptieService.Decrypt(b.Inhoud);
        }
        return geschiedenis;
    }

    public async Task<IEnumerable<Bericht>> GetBerichtenVoorAdminGedecodeerdAsync(int rapportId)
    {
        var rapport = await _chatRapportRepo.GetByIdAsync(rapportId);
        if (rapport == null)
        {
            throw new UnauthorizedAccessException("Geen geldig rapport gevonden.");
        }

        if (!rapport.ToestemmingGegeven)
        {
            throw new UnauthorizedAccessException("De gebruiker heeft geen toestemming gegeven om de chat te bekijken.");
        }

        var berichten = await _berichtRepo.GetBerichtenTussenGebruikersAsync(rapport.RapporteerderId, rapport.GerapporteerdeId, rapport.RapportageDatum);
        
        var decodedBerichten = berichten.ToList();
        foreach (var b in decodedBerichten)
        {
            b.Inhoud = _encryptieService.Decrypt(b.Inhoud);
        }
        return decodedBerichten;
    }

    public async Task<(bool Succes, string BerichtVolledig)> VerstuurBerichtAsync(string afzenderId, string ontvangerId, string inhoud)
    {
        // 1. Zorg ervoor dat het buddy's zijn
        var actieveKoppeling = await _buddyRepo.GetKoppelingAsync(afzenderId, ontvangerId) 
                            ?? await _buddyRepo.GetKoppelingAsync(ontvangerId, afzenderId);

        if (actieveKoppeling == null || !actieveKoppeling.IsActief)
        {
            return (false, "U bent geen buddy met deze persoon, u kunt geen berichten sturen.");
        }

        if (string.IsNullOrWhiteSpace(inhoud)) return (false, "Bericht mag niet leeg zijn.");
        if (inhoud.Length > 1000) return (false, "Bericht is te lang (max 1000 tekens).");

        // 2. Bericht structureren
        var nieuwBericht = new Bericht
        {
            AfzenderId = afzenderId,
            OntvangerId = ontvangerId,
            Inhoud = _encryptieService.Encrypt(inhoud),
            VerzondenOp = DateTime.UtcNow,
            IsGelezen = false
        };

        // 3. Opslaan in database
        await _berichtRepo.AddBerichtAsync(nieuwBericht);

        // 4. Stuur realtime bericht via externe Hub (voor mobiel/JS) indien actief
        await _notificatieService.StuurNieuwChatBerichtAsync(ontvangerId, new
        {
            AfzenderId = afzenderId,
            Inhoud = inhoud,
            VerzondenOp = nieuwBericht.VerzondenOp,
            Id = nieuwBericht.Id
        });
        
        // 5. Stuur realtime bericht via interne Event bus (Blazor Server circuits)
        _chatEventService.BroadcastNieuwBericht(ontvangerId, afzenderId);

        return (true, "Bericht is verzonden.");
    }

    public async Task MarkeerAlsGelezenAsync(string bekijkerId, string sprekerId)
    {
        await _berichtRepo.MarkeerAlsGelezenAsync(sprekerId, bekijkerId);
    }

    public async Task RapporteerGebruikerAsync(string rapporteerderId, string gerapporteerdeId, string toelichting, bool toestemming)
    {
        var rapport = new ChatRapport
        {
            RapporteerderId = rapporteerderId,
            GerapporteerdeId = gerapporteerdeId,
            Toelichting = toelichting,
            ToestemmingGegeven = toestemming,
            RapportageDatum = DateTime.UtcNow,
            IsAfgehandeld = false
        };

        await _chatRapportRepo.AddRapportAsync(rapport);
    }

    public async Task<IEnumerable<ChatRapport>> GetRapportenAsync()
    {
        return await _chatRapportRepo.GetRapportenAsync();
    }

    public async Task MarkeerRapportAlsAfgehandeldAsync(int rapportId)
    {
        var rapport = await _chatRapportRepo.GetByIdAsync(rapportId);
        if (rapport != null)
        {
            rapport.IsAfgehandeld = true;
            await _chatRapportRepo.UpdateAsync(rapport);
        }
    }

    public async Task<int> TotaalOngelezenAsync(string userId)
    {
        return await _berichtRepo.GetAantalOngelezenBerichtenAsync(userId);
    }

    public async Task<int> TotaalOngelezenVanAfzenderAsync(string ontvangerId, string afzenderId)
    {
        return await _berichtRepo.GetAantalOngelezenBerichtenVanAfzenderAsync(ontvangerId, afzenderId);
    }
}
