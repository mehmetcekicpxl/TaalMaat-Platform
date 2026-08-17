namespace TaalMaat.Core.Interfaces;

/// <summary>
/// Interface voor het versturen van realtime meldingen via SignalR
/// </summary>
public interface INotificatieService
{
    Task StuurNieuwBuddyVerzoekAsync(string ontvangerGebruikerId, object verzoekData);
    Task StuurBuddyVerzoekGeaccepteerdAsync(string verzenderGebruikerId, object data);
    Task StuurBuddyVerzoekAfgewezenAsync(string verzenderGebruikerId, object data);

    /// <summary>
    /// Stuurt een ping naar een specifieke gebruiker om bijvoorbeeld de Wachtkamer pagina te verversen.
    /// </summary>
    Task StuurAccountStatusUpdateAsync(string gebruikerId, string type);
    Task StuurNieuwChatBerichtAsync(string ontvangerId, object berichtData);


}
