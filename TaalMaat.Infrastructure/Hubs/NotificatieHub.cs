using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaalMaat.Infrastructure.Hubs;

/// <summary>
/// SignalR-hub voor realtime meldingen binnen TaalMaat.
/// Stuurt buddyverzoek-meldingen naar specifieke gebruikers.
/// </summary>
[Authorize]
public class NotificatieHub : Hub
{
    /// <summary>
    /// Stuurt een buddyverzoek-melding naar de ontvanger (Vrijwilliger).
    /// Wordt aangeroepen vanuit BuddyService.
    /// </summary>
    public static string GetGroupName(string gebruikerId) => $"gebruiker_{gebruikerId}";

    public override async Task OnConnectedAsync()
    {
        // Voeg de gebruiker toe aan zijn eigen groep op basis van zijn ID
        if (Context.UserIdentifier != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(Context.UserIdentifier));
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.UserIdentifier != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(Context.UserIdentifier));
        }
        await base.OnDisconnectedAsync(exception);
    }
}
