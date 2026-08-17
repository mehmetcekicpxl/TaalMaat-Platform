namespace TaalMaat.Application.Services;

/// <summary>
/// Singleton service om events (verstuurde berichten) over verschillende Blazor Server circuits (gebruikers-sessies) te zenden.
/// Vermijdt de noodzaak om SignalR HubConnectionBuilder binnen dezelfde server-applicatie te gebruiken.
/// </summary>
public class ChatEventService
{
    // Event parameters: OntvangerId, AfzenderId
    public event Action<string, string>? OnNieuwBericht;

    public void BroadcastNieuwBericht(string ontvangerId, string afzenderId)
    {
        OnNieuwBericht?.Invoke(ontvangerId, afzenderId);
    }
}
